using System.Collections.Generic;

namespace Godgame.model
{
    class Chest : Structure
    {
        public override string Path => "chest.png";
        public readonly IList<(Item Item, uint Amount)> items = new List<(Item, uint)>();

        public Chest(Tile tile) : base(1, tile) { }
    }
}
