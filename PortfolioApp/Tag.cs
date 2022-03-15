using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media.Imaging;


namespace PortfolioApp
{
    public class Tag //: INotifyPropertyChanged
    {

        //public event PropertyChangedEventHandler PropertyChanged;

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
            //OnPropertyChanged("Active");
        }

        //void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
