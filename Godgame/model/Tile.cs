using System.Collections.Generic;
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

        Dictionary<Direction, Tile> neighbours = new Dictionary<Direction, Tile>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Tile GetNeighbour(Direction dir)
        {
            Tile tile;
            neighbours.TryGetValue(dir, out tile);
            return tile;
        }

        public string Path => "grass.png";

        public void SetNeighbour(Direction dir, Tile neighbour)
        {
            if (neighbour != null) neighbours[dir] = neighbour;
        }
    }
}
