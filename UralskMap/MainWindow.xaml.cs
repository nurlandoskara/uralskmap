﻿using Microsoft.Maps.MapControl.WPF;
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
            ShowLocations(Enums.LocationType.Base);
        }

        private void ButtonSizes()
        {
            var height = SystemParameters.PrimaryScreenHeight / MenuPanel.Children.Count;
            MyMap.Margin = new Thickness(height + 10, 10, 10, 10);
            MenuTitles.Width = _width;
            MenuTitles.Margin = new Thickness(-_width, 0, _width, 0);
            int i = 0;
            foreach (Button button in MenuPanel.Children)
            {
                button.Width = height;
                button.Height = height;
                button.Click += MenuClick;
                button.Tag = i;
                i++;
            }
            MenuPanel.Width = height;
            i = 0;
            foreach (Button button in MenuTitles.Children)
            { 
                button.FontSize = 9;
                button.VerticalContentAlignment = VerticalAlignment.Center;
                button.HorizontalContentAlignment = HorizontalAlignment.Left;
                button.Height = height;
                button.Click += MenuClick;
                button.Tag = i;
                i++;
            }
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var locationType = (Enums.LocationType) button.Tag;
                ShowLocations(locationType);
            }
        }

        private void ShowLocations(Enums.LocationType locationType)
        {
            Title.Content = EnumHelper.Description(locationType);
            MyMap.ZoomLevel = locationType == Enums.LocationType.Base ? 7 : 12;
            MyMap.Center = locationType == Enums.LocationType.Base
                ? new Location(49.930999, 50.903586)
                : new Location(51.227339, 51.381180);
            var mapButtons = new MapButtons(25,25, MyMap);
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
