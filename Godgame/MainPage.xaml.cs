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
        const int tileSize = 100;

        World world = new World();

        public static IDictionary<string, BitmapImage> bitmapImages { get; private set; } = new Dictionary<string, BitmapImage>();

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

        private Image imageFromPath(IDrawable drawable)
        {
            var bitmapImage = bitmapImages[drawable.Path];
            var image = new Image();
            
            image.Source = bitmapImage;
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
            MainCanvas.Children.Add(btn);

            var canvas = new Canvas();
            btn.Content = canvas;

            var drawableConv = new DrawableToBitmapConverter();

            var tileImage = new Image();
            var structureImage = new Image();
            var actorImage = new Image();

            Binding tileBinding = new Binding();
            Binding structureBinding = new Binding();
            Binding actorBinding = new Binding();

            tileBinding.Converter = drawableConv;
            structureBinding.Converter = drawableConv;
            actorBinding.Converter = drawableConv;

            tileBinding.Path = new PropertyPath("");
            structureBinding.Path = new PropertyPath("Structure");
            actorBinding.Path = new PropertyPath("Actor");

            tileImage.SetBinding(Image.SourceProperty, tileBinding);
            structureImage.SetBinding(Image.SourceProperty, structureBinding);
            actorImage.SetBinding(Image.SourceProperty, actorBinding);

            tileImage.Width = tileSize;
            structureImage.Width = tileSize;
            actorImage.Width = tileSize;

            canvas.Children.Add(tileImage);
            canvas.Children.Add(structureImage);
            canvas.Children.Add(actorImage);
            

            //tileImage.Margin= new Thickness(0, 0, 0, 0);
            //canvas.Margin = new Thickness(0, 0, 0, 0);
            //tileImage.SetValue(Canvas.LeftProperty, 0);
            //tileImage.SetValue(Canvas.TopProperty, 0);

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            ((Tile)((Button)sender).DataContext).Structure = null;
        }

        public MainPage()
        {
            this.InitializeComponent();
            world.Fill();
            world.GetTile(new Coordinate(0, 0)).Structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).Structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).Actor = new Villager();

            CanvasInit();
        }
    }

    public class PathToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return MainPage.bitmapImages[(string)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
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
