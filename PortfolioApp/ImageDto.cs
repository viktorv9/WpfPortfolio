using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;


namespace PortfolioApp
{
    public class ImageDto
    {
        public ImageDto(int id, string title, string tags, string linkURL, byte[] data)
        {
            Id = id;
            Title = title;
            Tags = tags;
            LinkURL = linkURL;
            Data = data;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Tags { get; set; }

        public string LinkURL { get; set; }

        public byte[] Data { get; set; }

        public Image toImage()
        {
            var image = new BitmapImage();
            using (var ms = new System.IO.MemoryStream(Data))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
            }
            return new Image(Id, Title, Tags, LinkURL, image);
        }
    }
}
