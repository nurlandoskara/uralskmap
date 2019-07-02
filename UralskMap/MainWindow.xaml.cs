using System.Windows;
using Microsoft.Maps.MapControl.WPF;

namespace UralskMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myMap.Mode = new RoadMode();
            bgVideo.Play();
        }
    }
}
