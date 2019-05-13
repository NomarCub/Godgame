using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Godgame.Model.API
{
    public abstract class ItemContainerStructure : Structure
    {
        public ItemContainerStructure(Tile tile, params ItemAmount[] items) : base(tile)
        {
            _Items = new ObservableCollection<ItemAmount>(items);
            Items = new ReadOnlyObservableCollection<ItemAmount>(_Items);
        }

        public void ReceiveItemAmount(ItemAmount items)
        {
            foreach (var item in _Items)
            {
                if (item.Item == items.Item)
                {
                    item.Amount += items.Amount;
                    return;
                }
            }
            _Items.Add(items);
        }

        private readonly ObservableCollection<ItemAmount> _Items = new ObservableCollection<ItemAmount>();
        public ReadOnlyObservableCollection<ItemAmount> Items { get; }
        public override string Path => Items[0].Item.Path;

        public abstract override Task Interact();
    }
}
