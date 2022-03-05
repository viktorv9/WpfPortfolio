using System;
using System.Collections.Generic;
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

            client.BaseAddress = new Uri("http://localhost:5111/");

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
            imageViewModel.Images.Add(new Image(834794389, "hi", "hi", "hi", bitmap));
            ImageList.ItemsSource = imageViewModel.Images;
        }
    }
}
