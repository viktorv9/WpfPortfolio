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

            fetchImages();
        }

        private async void fetchImages()
        {
            imageViewModel.Images = await GetImages();
            imageViewModel.Tags = getTagsFromImages(imageViewModel.Images);
            ImageList.ItemsSource = imageViewModel.Images;
        }

        async Task<IList<Image>> GetImages()
        {
            return await Task.Run(async () =>
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:5111/images");
                List<ImageDto> DbImages = new List<ImageDto>();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    DbImages = JsonConvert.DeserializeObject<List<ImageDto>>(content);
                }
                var images = new List<Image>();
                foreach (ImageDto DbImage in DbImages)
                {
                    images.Add(DbImage.toImage());
                }
                return images;
            });
        }

        private List<Tag> getTagsFromImages(IList<Image> images)
        {
            List<Tag> tagList = new List<Tag>();
            foreach (Image image in images)
            {
                string[] tags = image.Tags.Split(',');
                tagList.AddRange(tags.Select(item => new Tag(item)).ToList());
            }
            return tagList;
        }

        private async void BtnUpload_Click(object sender, RoutedEventArgs e)
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
                imageViewModel.Images = await GetImages();
                ImageList.ItemsSource = imageViewModel.Images;
            }
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            Image clickedImage = (Image)((FrameworkElement)sender).DataContext;
            Console.WriteLine(clickedImage.Id);
        }
    }
}
