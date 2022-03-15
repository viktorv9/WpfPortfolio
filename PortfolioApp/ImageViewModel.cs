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
    class ImageViewModel : BindableBase
    {
        private HttpClient client = new HttpClient();

        private BindingList<Image> _ImageList;
        private BindingList<Tag> _TagList;

        public ImageViewModel()
        {
            _ImageList = new BindingList<Image>();
        }

        public BindingList<Image> Images
        {
            get { return _ImageList; }
            set
            {
                _ImageList = value;
            }
        }
        public BindingList<Tag> Tags
        {
            get { return _TagList; }
            set { _TagList = value; }
        }
        private async void UploadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                var bitmap = new BitmapImage(fileUri);

                byte[] data;
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }

                ImageDto image = new ImageDto(null, "Uploaded from app", "tags,test,upload", "https://google.com", data);
                var json = JsonConvert.SerializeObject(image);
                var jsonData = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://localhost:5111/images", jsonData);
                Images = await GetImages();
            }
        }

        public async Task<BindingList<Image>> GetImages()
        {
            return await Task.Run(async () =>
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:5111/images");
                BindingList<ImageDto> DbImages = new BindingList<ImageDto>();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    DbImages = JsonConvert.DeserializeObject<BindingList<ImageDto>>(content);
                }
                var images = new BindingList<Image>();
                foreach (ImageDto DbImage in DbImages)
                {
                    images.Add(DbImage.toImage());
                }
                return images;
            });
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.UploadImage(),
                        param => this.CanSave()
                    );
                }
                return _saveCommand;
            }
        }

        private bool CanSave()
        {
            return true;
        }

        public void AddImages(BindingList<Image> list)
        {
            foreach (Image img in list)
            {
                Images.Add(img);
            }
        }
    }
}
