using System;
using System.IO;
using System.Windows.Media.Imaging;
using UralskMap.Models;
using Caliburn.Micro;
using UralskMap.ViewModels;

namespace UralskMap.Views
{
    /// <summary>
    /// Interaction logic for WebViewer.xaml
    /// </summary>
    public partial class PhotoViewer
    {
        public PhotoViewer(string url)
        {
            InitializeComponent();
            var list = LoadData.GetFiles(url);
            var imagesList = new BindableCollection<ImageItem>();
            foreach (var image in list)
            {
                try
                {
                    imagesList.Add(new ImageItem
                        { Source = new BitmapImage(new Uri(image.FullPath)), Url = image.FullPath });
                }
                catch (Exception)
                {
                    continue;
                }
            }
            DataContext = new PhotoViewModel(imagesList);
        }
    }
}
