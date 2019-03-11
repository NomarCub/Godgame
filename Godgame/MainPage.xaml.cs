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

        private void initButton(Coordinate coordinate, Tile tile)
        {
            float X = (coordinate.x - world.MinCoordinate.x) * tileSize;
            float Y = (coordinate.y - world.MinCoordinate.y) * tileSize;

            Button btn = new Button();
            btn.SetValue(Canvas.LeftProperty, X);
            btn.SetValue(Canvas.TopProperty, Y);
            var bitmapImage = bitmapImages[tile.Path];
            var image = new Image();
            image.Width = bitmapImage.DecodePixelWidth = tileSize;
            image.Source = bitmapImage;
            btn.Content = image;

            btn.Padding = new Thickness(0, 0, 0, 0);
            btn.Margin = new Thickness(0, 0, 0, 0);
            MainCanvas.Children.Add(btn);
        }

        public MainPage()
        {
            this.InitializeComponent();
            world.Fill();
            world.GetTile(new Coordinate(0, 0)).structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).actor = new Villager();

            CanvasInit();
        }
    }
}
