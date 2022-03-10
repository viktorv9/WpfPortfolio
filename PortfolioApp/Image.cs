﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;


namespace PortfolioApp
{
    public class Image
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
    }
}
