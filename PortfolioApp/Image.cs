using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;


namespace PortfolioApp
{
    public class Image : INotifyPropertyChanged
    {
        public Image(int id, string title, List<string> tags, string linkURL, BitmapImage bitmapImg)
        {
            Id = id;
            Title = title;
            Tags = tags;
            LinkURL = linkURL;
            BitmapImg = bitmapImg;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public List<string> Tags { get; set; }

        public string LinkURL { get; set; }

        public BitmapImage BitmapImg { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TagsAsString
        {
            get
            {
                string result = "Tags: ";
                foreach (string tag in Tags)
                {
                    result = result + tag + ", ";
                }
                return result.Substring(0, result.Length - 2);
            }
        }

        public void NotifyPropetyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
