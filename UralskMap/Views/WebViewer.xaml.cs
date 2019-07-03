namespace UralskMap.Views
{
    /// <summary>
    /// Interaction logic for WebViewer.xaml
    /// </summary>
    public partial class WebViewer
    {
        public WebViewer(string url)
        {
            InitializeComponent();
            WebBrowser.Address = url;
        }
    }
}
