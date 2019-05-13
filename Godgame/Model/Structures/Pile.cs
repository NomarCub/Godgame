using Godgame.Model.API;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Godgame.Model.Structures
{
    class Pile : Structure
    {
        public Pile(Tile tile, params (Item Item, uint Amount)[] items) : base(0, tile)
        {
            Items = new ObservableCollection<(Item, uint)>(items);
        }

        public ObservableCollection<(Item Item, uint Amount)> Items { get; private set; } = new ObservableCollection<(Item, uint)>();
        public override string Path => Items[0].Item.Path;


        public async override Task Interact() { }
    }
}
