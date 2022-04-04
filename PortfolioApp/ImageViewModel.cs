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
using System.Collections.ObjectModel;
using PortfolioApp.Commands;

namespace PortfolioApp
{
    class ImageViewModel
    {
        private readonly HttpClient client = new HttpClient();

        private BindingList<Image> _Images;
        private BindingList<Tag> _Tags;

        private List<Image> prefilteredImageList = new List<Image>();

        public IDelegateCommand ToggleTagCommand { protected set; get; }
        public IDelegateCommand DeleteImageCommand { protected set; get; }
        public IDelegateCommand UploadImageCommand { protected set; get; }

        public ImageViewModel()
        {
            _Images = new BindingList<Image>();
            _Tags = new BindingList<Tag>();
            ToggleTagCommand = new DelegateCommand(ToggleTag);
            DeleteImageCommand = new DelegateCommand(DeleteImage);
            UploadImageCommand = new DelegateCommand(UploadImage);

            FetchImages();
        }

        public BindingList<Image> Images
        {
            get { return _Images; }
            set
            {
                _Images = value;
            }
        }

        public BindingList<Tag> Tags
        {
            get { return _Tags; }
            set { _Tags = value; }
        }

        private async void FetchImages()
        {
            Images.Clear();
            Tags.Clear();
            AddImages(await GetImages());
            AddTags(GetTagsFromImages(Images));
            prefilteredImageList = new List<Image>(Images);
        }

        private async Task<BindingList<Image>> GetImages()
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
                    images.Add(DbImage.ToImage());
                }
                return images;
            });
        }

        private BindingList<Tag> GetTagsFromImages(BindingList<Image> images)
        {
            List<string> tagnames = new List<string>();
            foreach (Image image in images)
            {
                tagnames.AddRange(image.Tags);
            }
            return new BindingList<Tag>(tagnames.Distinct().Select(tagname => new Tag(tagname)).ToList());
        }

        private async void UploadImage(object parameter)
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

                ImageDto image = new ImageDto(null, System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName), "aaaa,bbbb", "https://www.instagram.com/p/CMHkTBln27E/", data);
                var json = JsonConvert.SerializeObject(image);
                var jsonData = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://localhost:5111/images", jsonData);
                FetchImages();
            }
        }

        private async void DeleteImage(object parameter)
        {
            Image selectedImage = (Image)parameter;
            if (selectedImage != null)
            {
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:5111/images/" + selectedImage.Id);
                FetchImages();
            } else
            {
                MessageBox.Show("No image selected");
            }
        }

        private void ToggleTag(object parameter)
        {

            Tag clickedTag = (Tag)parameter;
            clickedTag.Toggle();
            BindingList<Image> filteredImages = sortImagesByTags(Tags);
            Images.Clear();
            AddImages(filteredImages);
        }

        private BindingList<Image> sortImagesByTags(BindingList<Tag> tagList)
        {
            List<Tag> activeTagList = tagList.Where(tag => tag.Active).ToList();
            if (activeTagList.Count() == 0) return new BindingList<Image>(prefilteredImageList);

            List<string> activeTagNames = activeTagList.Select(tag => tag.Name).ToList();
            List<Image> filteredImageList = new List<Image>();
            foreach (Image image in prefilteredImageList)
            {
                if (activeTagNames.Any(tag => image.Tags.Contains(tag))) filteredImageList.Add(image);
            }
            return new BindingList<Image>(filteredImageList);
        }

        private void AddImages(BindingList<Image> list)
        {
            foreach (Image img in list)
            {
                Images.Add(img);
            }
        }

        private void AddTags(BindingList<Tag> list)
        {
            foreach (Tag tg in list)
            {
                Tags.Add(tg);
            }
        }
    }
}
