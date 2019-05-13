using Godgame.Model.API;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Godgame.Model.Structures
{
    class Chest : Structure
    {
        public override string Path => "chest.png";
        public readonly IList<(Item Item, uint Amount)> items = new List<(Item, uint)>();

        public Chest(Tile tile) : base(1, tile) { }

        static Chest() { names[typeof(Chest)] = "Chest"; }

        public async override Task Interact() { }
    }
}
