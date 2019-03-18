using System.Collections.Generic;

namespace Godgame.model
{
    class Tile : IDrawable
    {
        //List<Structure> structures = new List<Structure>();
        //List<Actor> actors = new List<Actor>();
        public Structure Structure { get; set; } = null;
        public Actor Actor { get; set; } = null;
        Dictionary<Direction, Tile> neighbours = new Dictionary<Direction, Tile>();

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
