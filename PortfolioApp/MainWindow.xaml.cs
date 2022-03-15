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
using System.ComponentModel;

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageViewModel imageViewModel = new ImageViewModel();

        public MainWindow()
        {
            InitializeComponent();

            ImageList.ItemsSource = imageViewModel.Images;
            fetchImages();
        }

        private async void fetchImages()
        {
            imageViewModel.AddImages(await imageViewModel.GetImages());
            imageViewModel.Tags = getTagsFromImages(imageViewModel.Images);
        }

        private BindingList<Tag> getTagsFromImages(BindingList<Image> images)
        {
            List<string> tagnames = new List<string>();
            foreach (Image image in images)
            {
                tagnames.AddRange(image.Tags);
            }
            return new BindingList<Tag>(tagnames.Distinct().Select(tagname => new Tag(tagname)).ToList());
        }
    }
}
