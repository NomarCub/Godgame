using System.ComponentModel;

namespace Godgame.model
{
    class Tile : IDrawable, INotifyPropertyChanged
    {
        //List<Structure> structures = new List<Structure>();
        //List<Actor> actors = new List<Actor>();
        private Structure _Structure = null;
        public Structure Structure
        {
            get => _Structure;
            set
            {
                if (value != _Structure)
                {
                    _Structure = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Structure)));
                }
            }
        }

        private Actor _Actor = null;
        public Actor Actor
        {
            get => _Actor;
            set
            {
                if (value != _Actor)
                {
                    _Actor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Actor)));
                }
            }
        }

        public readonly Coordinate Coordinate;
        public readonly World World;

        public event PropertyChangedEventHandler PropertyChanged;

        public Tile(Coordinate coordinate, World world)
        {
            this.Coordinate = coordinate;
            this.World = world;
        }

        public Tile GetNeighbour(Direction dir)
        {
            return World[Coordinate.getNeighbour(dir)];
        }

        public bool Accept(Actor actor)
        {
            if (Actor == null)
            {
                actor.CurrentTile.Actor = null;
                actor.CurrentTile = this;
                Actor = actor;
                return true;
            }
            return false;
        }

        public string Path => "grass.png";
    }
}
