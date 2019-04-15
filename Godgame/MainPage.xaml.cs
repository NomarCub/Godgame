using Godgame.model;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Godgame
{
    public sealed partial class MainPage : Page
    {
        const int tileSize = 50;

        World world = World.GetTestWorld();

        public static IDictionary<string, BitmapImage> bitmapImages { get; private set; } = new Dictionary<string, BitmapImage>();
        public static DrawableToBitmapConverter DrawableToBitmapConverter = new DrawableToBitmapConverter();

        string[] imagePaths = { "grass.png", "villager.png", "tree.png", "void.png" };

        private void CanvasInit()
        {
            foreach (var path in imagePaths)
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("ms-appx:///Assets/images/" + path);
                bitmapImage.DecodePixelWidth = tileSize;
                bitmapImage.DecodePixelHeight = tileSize;
                bitmapImages[path] = bitmapImage;
            }

            for (int y = world.MinCoordinate.y; y <= world.MaxCoordinate.y; y++)
            {
                for (int x = world.MinCoordinate.x; x <= world.MaxCoordinate.x; x++)
                {
                    var currentCoordinate = new Coordinate(x, y);
                    Tile currentTile = world.GetTile(currentCoordinate);
                    if (currentTile != null)
                    {
                        initButton(currentCoordinate, currentTile);
                    }
                }
            }

        }

        private Image imageFromPropertyName(string propertyName)
        {
            var image = new Image();

            Binding binding = new Binding();
            binding.Converter = DrawableToBitmapConverter;
            binding.Path = new PropertyPath(propertyName);
            image.SetBinding(Image.SourceProperty, binding);
            image.Width = tileSize;
            image.Height = tileSize;

            //image.Margin = new Thickness(0, 0, 0, 0);
            //image.SetValue(Canvas.LeftProperty, 0);
            //image.SetValue(Canvas.TopProperty, 0);

            return image;
        }

        private void initButton(Coordinate coordinate, Tile tile)
        {
            Button btn = new Button();
            btn.DataContext = tile;
            btn.Click += Btn_Click;
            float X = (coordinate.x - world.MinCoordinate.x) * tileSize;
            float Y = (coordinate.y - world.MinCoordinate.y) * tileSize;
            btn.SetValue(Canvas.LeftProperty, X);
            btn.SetValue(Canvas.TopProperty, Y);
            btn.Padding = new Thickness(0, 0, 0, 0);
            btn.Margin = new Thickness(0, 0, 0, 0);
            btn.BorderThickness = new Thickness(0, 0, 0, 0);
            MainCanvas.Children.Add(btn);

            var canvas = new Canvas();
            btn.Content = canvas;

            canvas.Children.Add(imageFromPropertyName(""));
            canvas.Children.Add(imageFromPropertyName(nameof(Tile.Structure)));
            canvas.Children.Add(imageFromPropertyName(nameof(Tile.Actor)));

            //canvas.Margin = new Thickness(0, 0, 0, 0);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            ((sender as Button).DataContext as Tile).Structure = null;
        }

        public MainPage()
        {
            this.InitializeComponent();

            CanvasInit();

            DispatcherTimer ticker = new DispatcherTimer();
            ticker.Interval = new TimeSpan(0, 0, 0, 0, 500);
            ticker.Tick += Test;
            ticker.Start();
        }

        private void Test(object sender, object e)
        {
            if (world.GetTile(new Coordinate(0, 0)).Structure == null)
                world.GetTile(new Coordinate(0, 0)).Structure = new Tree();
            else world.GetTile(new Coordinate(0, 0)).Structure = null;
        }
    }
    public class DrawableToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return MainPage.bitmapImages["void.png"];
            else
                return MainPage.bitmapImages[((IDrawable)value).Path];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
