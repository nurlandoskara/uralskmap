using System;

namespace UralskMap.Views
{
    /// <summary>
    /// Interaction logic for WebViewer.xaml
    /// </summary>
    public partial class VideoViewer
    {
        public VideoViewer(string url)
        {
            InitializeComponent();
            Video.Source = new Uri(url);
            Video.Play();
        }
    }
}
