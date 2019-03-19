using Godgame.model;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Godgame
{
    public sealed partial class MainPage : Page
    {
        const int tileSize = 100;

        World world = new World();

        Dictionary<string, BitmapImage> bitmapImages = new Dictionary<string, BitmapImage>();
        string[] imagePaths = { "grass.png", "villager.png", "tree.png" };

        private void CanvasInit()
        {
            foreach (var path in imagePaths)
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri("ms-appx:///Assets/images/" + path);
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

        private Image imageFromPath(string path)
        {
            var bitmapImage = bitmapImages[path];
            var image = new Image();
            image.Width = bitmapImage.DecodePixelWidth = tileSize;
            image.Source = bitmapImage;
            return image;
        }

        private void initButton(Coordinate coordinate, Tile tile)
        {
            var canvas = new Canvas();

            var tileImage = imageFromPath(tile.Path);

            canvas.Children.Add(tileImage);
            if (tile.Structure != null)
                canvas.Children.Add(imageFromPath(tile.Structure.Path));
            if (tile.Actor != null)
                canvas.Children.Add(imageFromPath(tile.Actor.Path));
            //tileImage.Margin= new Thickness(0, 0, 0, 0);
            //canvas.Margin = new Thickness(0, 0, 0, 0);
            //tileImage.SetValue(Canvas.LeftProperty, 0);
            //tileImage.SetValue(Canvas.TopProperty, 0);

            Button btn = new Button();
            btn.Content = canvas;
            float X = (coordinate.x - world.MinCoordinate.x) * tileSize;
            float Y = (coordinate.y - world.MinCoordinate.y) * tileSize;
            btn.SetValue(Canvas.LeftProperty, X);
            btn.SetValue(Canvas.TopProperty, Y);
            btn.Padding = new Thickness(0, 0, 0, 0);
            btn.Margin = new Thickness(0, 0, 0, 0);
            MainCanvas.Children.Add(btn);
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
}
