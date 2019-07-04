using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Microsoft.Maps.MapControl.WPF;
using UralskMap.Views;
using LocationPoint = UralskMap.Models.LocationPoint;

namespace UralskMap
{
    public class MapButtons
    {
        private bool _isExpanding;
        private List<LocationPoint> _items;
        private Map _map;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public MapButtons(int width, int height, Map map)
        {
            Width = width;
            Height = height;
            _map = map;
            _map.MouseDown += Map_OnMouseDown;
        }

        public List<Canvas> GetList(Enums.LocationType locationType)
        {
            var list = new List<Canvas>();
            _items = Locations.GetLocations(locationType);
            foreach (var location in _items)
            {
                list.Add(Get(location));
            }

            return list;
        }
        public Canvas Get(LocationPoint location)
        {
            var canvas = new Canvas { Width = Width * 5, Height = Width * 2 };
            var brush = new ImageBrush
            {
                ImageSource = new BitmapImage(
                    new Uri(
                        $"pack://application:,,,/UralskMap;component/Resources/LocationTypeIcons/{location.LocationType.ToString()}.jpg")),
                Stretch = Stretch.Uniform
            };
            var point = new Border
            {
                Background = brush,
                Height = Width,
                Width = Width,
                Tag = location.Id,
            };
            point.MouseDown += PointOnClick;
            Canvas.SetLeft(point, Width + Width / 2);
            Canvas.SetTop(point, Width * 2);
            for (var i = 0; i < 5; i++)
            {
                var button = new Border
                {
                    Width = Width,
                    Height = Width,
                    Background = new SolidColorBrush(Colors.Black),
                    CornerRadius = new CornerRadius(30),
                    Tag = location.Id
                };
                var bg = new ImageBrush();
                switch (i)
                {
                    case 0:
                        bg.ImageSource = new BitmapImage(
                            new Uri("pack://application:,,,/UralskMap;component/Resources/ButtonIcons/info.png"));
                        break;
                    case 1:
                        button.MouseDown += Video_OnMouseDown;
                        bg.ImageSource = new BitmapImage(
                            new Uri("pack://application:,,,/UralskMap;component/Resources/ButtonIcons/video.png"));
                        break;
                    case 2:
                        button.MouseDown += Pano_OnMouseDown;
                        bg.ImageSource = new BitmapImage(
                            new Uri("pack://application:,,,/UralskMap;component/Resources/ButtonIcons/360.png"));
                        break;
                    case 3:
                        button.MouseDown += Model_OnMouseDown; bg.ImageSource = new BitmapImage(
                    new Uri("pack://application:,,,/UralskMap;component/Resources/ButtonIcons/3d.png"));
                        break;
                    case 4:
                        button.MouseDown += Photo_OnMouseDown;
                        bg.ImageSource = new BitmapImage(
                            new Uri("pack://application:,,,/UralskMap;component/Resources/ButtonIcons/photo.png"));
                        break;
                }

                bg.Stretch = Stretch.Uniform;
                button.Background = bg;
                Canvas.SetLeft(button, Width + Width / 2);
                Canvas.SetTop(button, Width * 2);
                canvas.Children.Add(button);
            }

            canvas.Children.Add(point);
            var text = new TextBlock
            {
                Text = location.Title,
                Visibility = Visibility.Hidden,
                Background = new SolidColorBrush(Colors.CornflowerBlue),
                Padding = new Thickness(5),
                Foreground = new SolidColorBrush(Colors.White),
                TextWrapping = TextWrapping.Wrap,
                Width = Width * 5
            };
            Canvas.SetLeft(text, 0);
            Canvas.SetTop(text, Width * 3);
            canvas.Children.Add(text);

            MapLayer.SetPosition(canvas, new Location(location.Position.Latitude, location.Position.Longitude));
            return canvas;
        }

        private void Video_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new VideoViewer($"C:\\xampp\\htdocs\\locations\\{button.Tag}\\video.mp4");
            view.ShowDialog();
        }
        private void Pano_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new WebViewer($"http:\\localhost\\locations\\{button.Tag}\\pano.html");
            view.ShowDialog();
        }
        private void Model_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new WebViewer($"http:\\localhost\\locations\\{button.Tag}\\3d model\\start.html");
            view.ShowDialog();
        }

        private void Photo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new PhotoViewer($"C:\\xampp\\htdocs\\locations\\{button.Tag}\\photo");
            view.ShowDialog();
        }

        private void PointOnClick(object sender, RoutedEventArgs e)
        {
            _isExpanding = true;
            var point = (sender as Border);
            var item = _items.FirstOrDefault(x => point != null && x.Id == (int) point.Tag);
            if (item == null) return;
            var parent = point?.Parent as Canvas;
            if (parent == null) return;
            Panel.SetZIndex(parent, 1000);
            var text = parent.Children.OfType<TextBlock>().FirstOrDefault();
            if (text != null) text.Visibility = Visibility.Visible;

            double a = 0;
            foreach (Border button in parent.Children)
            {
                if (a > Math.PI) break;
                button.Visibility = Visibility.Visible;
                var radius = Width + Width / 2;
                var leftAnimation = new DoubleAnimation
                {
                    From = radius,
                    To = radius - radius * Math.Cos(a),
                    Duration = TimeSpan.FromSeconds(1)
                };

                var topAnimation = new DoubleAnimation
                {
                    From = radius,
                    To = radius - radius * Math.Sin(a),
                    Duration = TimeSpan.FromSeconds(1)
                };

                button.BeginAnimation(Canvas.LeftProperty, leftAnimation);
                button.BeginAnimation(Canvas.TopProperty, topAnimation);

                a += Math.PI / 4;
            }
        }


        private void Map_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isExpanding)
            {
                _isExpanding = false;
                return;
            }
            foreach (Canvas canvas in _map.Children)
            {
                var text = canvas.Children.OfType<TextBlock>().FirstOrDefault();
                if (text != null) text.Visibility = Visibility.Hidden;
                double a = 0;
                foreach (Border button in canvas.Children)
                {
                    if (a > Math.PI) break;
                    var radius = Width + Width / 2;
                    var leftAnimation = new DoubleAnimation
                    {
                        From = radius - radius * Math.Cos(a),
                        To = radius,
                        Duration = TimeSpan.FromSeconds(1)
                    };

                    var topAnimation = new DoubleAnimation
                    {
                        From = radius - radius * Math.Sin(a),
                        To = radius,
                        Duration = TimeSpan.FromSeconds(1)
                    };

                    button.BeginAnimation(Canvas.LeftProperty, leftAnimation);
                    button.BeginAnimation(Canvas.TopProperty, topAnimation);

                    button.Visibility = Visibility.Hidden;
                    a += Math.PI / 4;
                }
                Panel.SetZIndex(canvas, 0);
            }

            _isExpanding = false;
        }
    }
}
