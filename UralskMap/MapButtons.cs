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
using Location = UralskMap.Models.Location;

namespace UralskMap
{
    public class MapButtons
    {
        private bool _isExpanding;
        private List<Location> _items;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public MapButtons(int width, int height)
        {
            Width = width;
            Height = height;
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
        public Canvas Get(Location location)
        {
            var canvas = new Canvas { Width = Width * 5, Height = Width * 2 };
            var brush = new ImageBrush
            {
                ImageSource = new BitmapImage(
                    new Uri(
                        $"pack://application:,,,/AktobeInteractive;component/Resources/LocationTypeIcons/{EnumHelper.Description(location.LocationType)}.jpg")),
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
                            new Uri("pack://application:,,,/AktobeInteractive;component/Resources/ButtonIcons/info.png"));
                        break;
                    case 1:
                        button.MouseDown += Video_OnMouseDown;
                        bg.ImageSource = new BitmapImage(
                            new Uri("pack://application:,,,/AktobeInteractive;component/Resources/ButtonIcons/video.png"));
                        break;
                    case 2:
                        button.MouseDown += Pano_OnMouseDown;
                        bg.ImageSource = new BitmapImage(
                            new Uri("pack://application:,,,/AktobeInteractive;component/Resources/ButtonIcons/360.png"));
                        break;
                    case 3:
                        button.MouseDown += Model_OnMouseDown; bg.ImageSource = new BitmapImage(
                    new Uri("pack://application:,,,/AktobeInteractive;component/Resources/ButtonIcons/3d.png"));
                        break;
                    case 4:
                        button.MouseDown += Photo_OnMouseDown;
                        bg.ImageSource = new BitmapImage(
                            new Uri("pack://application:,,,/AktobeInteractive;component/Resources/ButtonIcons/photo.png"));
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
                Background = new SolidColorBrush(Colors.Black),
                Padding = new Thickness(5),
                Foreground = new SolidColorBrush(Colors.White),
                TextWrapping = TextWrapping.Wrap,
                Width = Width * 5
            };
            Canvas.SetLeft(text, 0);
            Canvas.SetTop(text, Width * 3);
            canvas.Children.Add(text);

            MapLayer.SetPosition(canvas, location.Position);
            return canvas;
        }

        private void Video_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new VideoViewer($"C:\\xampp\\htdocs\\{button.Tag}\\video.mp4");
            view.ShowDialog();
        }
        private void Pano_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new WebViewer($"http:\\localhost\\{button.Tag}\\pano.html");
            view.ShowDialog();
        }
        private void Model_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new WebViewer($"http:\\localhost\\{button.Tag}\\3d model\\start.html");
            view.ShowDialog();
        }

        private void Photo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Border;
            if (button == null) return;
            var view = new PhotoViewer($"C:\\xampp\\htdocs\\{button.Tag}\\photo");
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
    }
}
