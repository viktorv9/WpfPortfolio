using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;


namespace PortfolioApp
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
            Active = false;
        }

        public string Name { get; set; }
        public bool Active { get; set; }

        public void Toggle()
        {
            Active = !Active;
        }
    }
}
