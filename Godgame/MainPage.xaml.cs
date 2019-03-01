using Godgame.model;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Godgame
{
    public sealed partial class MainPage : Page
    {
        World world = new World();
        Dictionary<string, CanvasBitmap> images = new Dictionary<string, CanvasBitmap>();
        string[] imagePaths = { "grass.png", "villager.png", "tree.png" };

        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            foreach (var path in imagePaths)
            {
                var image = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/images/" + path));
                images[path] = image;
            }
        }
        private void Canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private void Canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            //throw new NotImplementedException();
        }

        private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(Canvas).Position;
            //var isMouse = e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse;
            var isLeft = e.GetCurrentPoint(this).Properties.IsLeftButtonPressed;
        }

        private void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            for (int y = world.MinCoordinate.y; y <= world.MaxCoordinate.y; y++)
            {
                for (int x = world.MinCoordinate.x; x <= world.MaxCoordinate.x; x++)
                {
                    var currentCoordinate = new Coordinate(x, y);
                    Tile currentTile = world.GetTile(currentCoordinate);
                    if (currentTile != null)
                    {
                        DrawOnCoordinate(currentCoordinate, currentTile.Path, args);
                        if (currentTile.structure != null)
                            DrawOnCoordinate(currentCoordinate, currentTile.structure.Path, args);
                        if (currentTile.actor != null)
                            DrawOnCoordinate(currentCoordinate, currentTile.actor.Path, args);
                    }
                    else
                    {
                        //'?'
                    }
                }
            }

        }

        private void DrawOnCoordinate(Coordinate coordinate, string path, CanvasAnimatedDrawEventArgs args)
        {
            float X = (coordinate.x - world.MinCoordinate.x) * 100;
            float Y = (coordinate.y - world.MinCoordinate.y) * 100;
            args.DrawingSession.DrawImage(images[path], X, Y);
        }

        public MainPage()
        {
            this.InitializeComponent();
            world.Fill();
            world.GetTile(new Coordinate(0, 0)).structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).actor = new Villager();
        }
    }
}
