using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace PortfolioApp
{
    public class Image
    {
        public int ImageId { get; set; }

        public string ImageTitle { get; set; }

        public IList<string> ImageTags { get; set; }

        public string ImageLink { get; set; }
    }
}
