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
        }

        private void Image_Click(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = (Image)((FrameworkElement)sender).DataContext;
            if (e.ClickCount == 2 && !(clickedImage.LinkURL == null || clickedImage.LinkURL == ""))
            {
                System.Diagnostics.Process.Start(clickedImage.LinkURL);
            }
        }
    }
}
