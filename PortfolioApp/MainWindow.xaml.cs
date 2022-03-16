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
    public partial class ImageView : Window
    {
        ImageViewModel imageViewModel = new ImageViewModel();

        public ImageView()
        {
            InitializeComponent();

            ImageList.ItemsSource = imageViewModel.Images;
            imageViewModel.fetchImages();
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            imageViewModel.UploadImage();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Image selectedImage = (Image)ImageList.SelectedItem;
            if (selectedImage != null) imageViewModel.DeleteImage(selectedImage);
            else MessageBox.Show("No image selected!");
        }

        private void Image_Click(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = (Image)((FrameworkElement)sender).DataContext;
            if (e.ClickCount == 2 && clickedImage.LinkURL != null)
            {
                System.Diagnostics.Process.Start(clickedImage.LinkURL);
            }
        }
    }
}
