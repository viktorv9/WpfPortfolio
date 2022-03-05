﻿using System;
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
            _ImageList = new List<Image>();
        }

        public IList<Image> Images
        {
            get { return _ImageList; }
            set { _ImageList = value; }
        }
    }
}
