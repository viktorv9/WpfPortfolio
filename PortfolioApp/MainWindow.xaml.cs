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

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Image> MyItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();


            MyItems = new List<Image>();
            Image newItem = new Image();
            //newItem.Image = ... load BMP here...;
            newItem.ImageTitle = "FooBar Icon";
            MyItems.Add(newItem);
            Image newItem2 = new Image();
            //newItem.Image = ... load BMP here...;
            newItem2.ImageTitle = "Icon22";
            MyItems.Add(newItem2);

            myListBox.ItemsSource = MyItems;
        }
    }
}
