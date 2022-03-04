using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace PortfolioApp
{
    class ImageViewModel : BindableBase
    {
        private IList<Image> _ImageList;

        public ImageViewModel()
        {
            _ImageList = new List<Image>
            {
                // todo: database query here to get all images
                new Image{Id = 0, Title="Stiletto", LinkURL="https://www.instagram.com/p/CX7Ace1oiUk/"},
                new Image{Id = 1, Title="Ibara Shiozaki", LinkURL="https://www.instagram.com/p/CMHkTBln27E/"}
            };
        }

        public IList<Image> Images
        {
            get { return _ImageList; }
            set { _ImageList = value; }
        }
    }
}
