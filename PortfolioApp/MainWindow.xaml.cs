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
using Newtonsoft.Json;

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Image> Images { get; set; }

        HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:5111/");

            GetImages();
        }

        async void GetImages()
        {
            List<Image> images = null;
            HttpResponseMessage response = await client.GetAsync("http://localhost:5111/images");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                images = JsonConvert.DeserializeObject<List<Image>>(content);
            }
            myListBox.ItemsSource = images;
        }
    }
}
