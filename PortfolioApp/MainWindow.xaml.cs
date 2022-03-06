using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        ImageViewModel imageViewModel = new ImageViewModel();
        BitmapImage bitmap = new BitmapImage();

        public MainWindow()
        {
            InitializeComponent();

            GetImages();

            //temp load an image (delete chrissketch en andere testimages later)
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("F:/GitHub-Reps/WpfPortfolio/PortfolioApp/chrissketch.png");
            bitmap.EndInit();
        }

        async void GetImages()
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:5111/images");
            List<ImageDto> DbImages = new List<ImageDto>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                DbImages = JsonConvert.DeserializeObject<List<ImageDto>>(content);
            }
            foreach(ImageDto DbImage in DbImages)
            {
                imageViewModel.Images.Add(DbImage.toImage());
            }
            ImageList.ItemsSource = imageViewModel.Images;
        }

        private async void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                var bitmap = new BitmapImage(fileUri);

                byte[] data;
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                ImageDto image = new ImageDto(null, "Uploaded from app", "tags,test,upload", "https://google.com", data);
                var json = JsonConvert.SerializeObject(image);
                var jsonData = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://localhost:5111/images", jsonData);
                Console.WriteLine(response);
                GetImages();
                Console.WriteLine(3);
            }
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            Image clickedImage = (Image)((FrameworkElement)sender).DataContext;
            Console.WriteLine(clickedImage.Id);
        }
    }
}
