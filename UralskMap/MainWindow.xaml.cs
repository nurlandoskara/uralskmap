using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace UralskMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _isExpand;
        private int _width = 200;
        public MainWindow()
        {
            InitializeComponent();
            MyMap.Mode = new RoadMode();
            BgVideo.Source = new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/bg.mp4");
            BgVideo.Play();
            MyMap.Focus();
            ButtonSizes();
        }

        private void ButtonSizes()
        {
            var height = SystemParameters.PrimaryScreenHeight / MenuPanel.Children.Count;
            MenuTitles.Width = _width;
            MenuTitles.Margin = new Thickness(-_width, 0, _width, 0);
            foreach (Button button in MenuPanel.Children)
            {
                button.Width = height;
                button.Height = height;
            }
            MenuPanel.Width = height;
            foreach (Button button in MenuTitles.Children)
            {
                button.FontSize = 8;
                button.VerticalContentAlignment = VerticalAlignment.Center;
                button.HorizontalContentAlignment = HorizontalAlignment.Left;
                button.Height = height;
            }
        }

        private void ShowLocations(Enums.LocationType locationType)
        {
            var mapButtons = new MapButtons(5,5, MyMap);
            var canvasList = mapButtons.GetList(locationType);
            foreach (var canvas in canvasList)
            {
                MyMap.Children.Add(canvas);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var height = SystemParameters.PrimaryScreenHeight / MenuPanel.Children.Count;
            var expandThickness = new Thickness(height, 0, -height, 0);
            var hideThickness = new Thickness(-_width, 0, _width, 0);
            if (_isExpand)
            {
                var animation = new ThicknessAnimation { From = expandThickness, To = hideThickness, Duration = TimeSpan.FromSeconds(1)};
                MenuTitles.BeginAnimation(MarginProperty, animation);
                _isExpand = false;
            }
            else
            {
                var animation = new ThicknessAnimation { From = hideThickness, To = expandThickness, Duration = TimeSpan.FromSeconds(1) };
                MenuTitles.BeginAnimation(MarginProperty, animation);
                _isExpand = true;
            }
        }
        
    }
}
