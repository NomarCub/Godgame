using Godgame.Model.API;
using System.Threading.Tasks;

namespace Godgame.Model.Structures
{
    class Pile : ItemContainerStructure
    {
        static Pile()
        {
            names[typeof(Pile)] = "Pile";
            AllMaxHitPoints[typeof(Pile)] = 1;
        }
        public Pile(Tile tile, params ItemAmount[] items) : base(tile, items) { }

        public override string Path => Items[0].Item.Path;

        public async override Task Interact() { }
    }
}
