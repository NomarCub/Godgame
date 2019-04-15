using System.Collections.Generic;

namespace Godgame.model
{

    class World
    {
        Dictionary<Coordinate, Tile> tiles = new Dictionary<Coordinate, Tile>();

        private bool isUpToDate = true;

        private Coordinate minCoor;
        private Coordinate maxCoor;
        public Coordinate MinCoordinate
        {
            get
            {
                if (!isUpToDate) update();
                return minCoor;
            }
            private set
            {
                minCoor = value;
            }
        }
        public Coordinate MaxCoordinate
        {
            get
            {
                if (!isUpToDate) update();
                return maxCoor;
            }
            private set
            {
                maxCoor = value;
            }
        }

        private void update()
        {
            var coordinates = new List<Coordinate>(tiles.Keys);
            int minX = coordinates[0].x, maxX = coordinates[0].x, minY = coordinates[0].y, maxY = coordinates[0].y;
            foreach (var coor in coordinates)
            {
                if (coor.x < minX)
                    minX = coor.x;
                if (coor.y < minY)
                    minY = coor.y;
                if (coor.x > maxX)
                    maxX = coor.x;
                if (coor.y > maxY)
                    maxY = coor.y;
            }
            MinCoordinate = new Coordinate(minX, minY);
            MaxCoordinate = new Coordinate(maxX, maxY);

            isUpToDate = true;
        }

        //public void print()
        //{
        //    if (!upToDate)
        //        update();

        //    for (int y = minCoordinate.y; y <= maxCoordinate.y; y++)
        //    {
        //        for (int x = minCoordinate.x; x <= maxCoordinate.x; x++)
        //        {
        //            var currentCoordinate = new Coordinate(x, y);

        //            Tile currentTile;
        //            if (tiles.TryGetValue(currentCoordinate, out currentTile))
        //            {
        //                System.Console.Write(currentTile.print); ;
        //            }
        //            else
        //            {
        //                System.Console.Write('?');
        //            }
        //        }
        //        System.Console.WriteLine();
        //    }
        //    System.Console.WriteLine();
        //}

        public void putTile(Coordinate coordinate, Tile tile)
        {
            tiles[coordinate] = tile;
            Tile up, down, left, right;

            tiles.TryGetValue(coordinate.getNeighbour(Direction.up), out up);
            tiles.TryGetValue(coordinate.getNeighbour(Direction.down), out down);
            tiles.TryGetValue(coordinate.getNeighbour(Direction.left), out left);
            tiles.TryGetValue(coordinate.getNeighbour(Direction.right), out right);

            up?.SetNeighbour(Direction.down, tile);
            down?.SetNeighbour(Direction.up, tile);
            left?.SetNeighbour(Direction.right, tile);
            right?.SetNeighbour(Direction.left, tile);

            tile.SetNeighbour(Direction.up, up);
            tile.SetNeighbour(Direction.down, down);
            tile.SetNeighbour(Direction.left, left);
            tile.SetNeighbour(Direction.right, right);

            isUpToDate = false;
        }

        public Tile GetTile(Coordinate coordinate)
        {
            Tile tile;
            if (tiles.TryGetValue(coordinate, out tile))
                return tile;
            else return null;
        }

        public void Fill(int X = 20, int Y = 20)
        {
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    putTile(new Coordinate(x, y), new Tile());
                }
            }
        }

        public static World GetTestWorld()
        {
            var world = new World();
            world.Fill();
            world.GetTile(new Coordinate(0, 0)).Structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).Structure = new Tree();
            world.GetTile(new Coordinate(2, 3)).Actor = new Villager();
            return world;
        }
    }
}
