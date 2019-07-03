using System;
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
            MyMap.Mode = new RoadMode();
            BgVideo.Source = new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/bg.mp4");
            BgVideo.Play();
            MyMap.Focus();
        }

        private void ShowLocations(Enums.LocationType locationType)
        {
            var mapButtons = new MapButtons(5,5);
            var canvasList = mapButtons.GetList(locationType);
            foreach (var canvas in canvasList)
            {
                MyMap.Children.Add(canvas);
            }
        }
    }
}
