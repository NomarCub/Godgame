using Godgame.Model.Structures;
using Godgame.Model.Tiles;
using System.Threading.Tasks;

namespace Godgame.Model.API
{
    public delegate Task ContainerInteractHandler(ItemContainerStructure container);

    public class World
    {
        public readonly Coordinate MaxCoordinate;

        private readonly Tile[][] tiles;

        public event ContainerInteractHandler ContainerInteractEvent;

        public void ContainerEvent(ItemContainerStructure container)
        {
            ContainerInteractEvent?.Invoke(container);
        }

        public World(Coordinate maxCoor)
        {
            MaxCoordinate = maxCoor;
            tiles = new Tile[MaxCoordinate.x + 1][];
            for (int i = 0; i < MaxCoordinate.x + 1; i++)
            {
                tiles[i] = new Tile[MaxCoordinate.y + 1];
            }
        }

        public Tile this[int x, int y]
        {
            get { return tiles[x][y]; }
            private set { tiles[x][y] = value; }
        }

        public Tile this[Coordinate coordinate]
        {
            get { return tiles[coordinate.x][coordinate.y]; }
            private set { tiles[coordinate.x][coordinate.y] = value; }
        }

        public void PutActor(Actor actor, Coordinate coordinate)
        {
            var tile = this[coordinate];
            tile.Actor = actor;
            actor.CurrentTile = tile;
        }

        public void Fill()
        {
            for (int x = 0; x < MaxCoordinate.x; x++)
            {
                for (int y = 0; y < MaxCoordinate.y; y++)
                {
                    var coor = new Coordinate(x, y);
                    this[coor] = new Ground(coor, this);
                }
            }
        }

        public static World GetTestWorld()
        {
            var world = new World(new Coordinate(25, 25));
            world.Fill();
            world[0, 0].Structure = new Tree(world[0, 0]);
            world[2, 3].Structure = new Tree(world[2, 3]);

            for (int x = 0; x < world.MaxCoordinate.x; x++)
            {
                var coor = new Coordinate(x, 9);
                world[coor] = new Water(coor, world);
            }

            return world;
        }
    }
}
