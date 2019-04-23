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
        Villager villager = new Villager();

        public static IDictionary<string, BitmapImage> bitmapImages { get; private set; } = new Dictionary<string, BitmapImage>();
        public static DrawableToBitmapConverter DrawableToBitmapConverter = new DrawableToBitmapConverter();

        List<string> imagePaths = new List<string>();

        private async void CanvasInitAsync()
        {
            var imagesFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\images");
            var imageFiles = await imagesFolder.CreateItemQuery().GetItemsAsync();
            foreach (var file in imageFiles)
                imagePaths.Add(file.Name);

            foreach (var path in imagePaths)
            {
                var bitmapImage = new BitmapImage();

                bitmapImage.UriSource = new Uri("ms-appx:///Assets/images/" + path);
                bitmapImage.DecodePixelWidth = tileSize;
                bitmapImage.DecodePixelHeight = tileSize;
                bitmapImages[path] = bitmapImage;
            }

            for (int y = 0; y <= world.MaxCoordinate.y; y++)
            {
                for (int x = 0; x <= world.MaxCoordinate.x; x++)
                {
                    var currentCoordinate = new Coordinate(x, y);
                    Tile currentTile = world[currentCoordinate];
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
            float X = coordinate.x * tileSize;
            float Y = coordinate.y * tileSize;
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
            var tile = (sender as Button).DataContext as Tile;
            int dx = tile.Coordinate.x - villager.CurrentTile.Coordinate.x;
            int dy = tile.Coordinate.y - villager.CurrentTile.Coordinate.y;
            var move = new Coordinate(Math.Sign(dx), Math.Sign(dy));

            world[villager.CurrentTile.Coordinate + move].Accept(villager);
        }

        public MainPage()
        {
            this.InitializeComponent();
            world.PutActor(villager, new Coordinate(3, 3));

            CanvasInitAsync();

            DispatcherTimer ticker = new DispatcherTimer();
            ticker.Interval = new TimeSpan(0, 0, 0, 0, 500);
            ticker.Tick += Test;
            ticker.Start();
        }

        private void Test(object sender, object e)
        {
            if (world[0, 0].Structure == null)
                world[0, 0].Structure = new Tree();
            else world[0, 0].Structure = null;
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
