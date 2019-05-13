using System.Collections.ObjectModel;

namespace Godgame.Model.API
{
    public abstract class ItemContainerStructure : Structure
    {
        public ItemContainerStructure(Tile tile, params ItemAmount[] items) : base(tile)
        {
            _Items = new ObservableCollection<ItemAmount>(items);
            Items = new ReadOnlyObservableCollection<ItemAmount>(_Items);
        }

        public void ReceiveItemAmount(ItemAmount newItemAmount)
        {
            foreach (var itemAmount in _Items)
            {
                if (itemAmount.Item == newItemAmount.Item)
                {
                    itemAmount.Amount += newItemAmount.Amount;
                    return;
                }
            }
            _Items.Add(newItemAmount);
        }

        private readonly ObservableCollection<ItemAmount> _Items = new ObservableCollection<ItemAmount>();
        public ReadOnlyObservableCollection<ItemAmount> Items { get; }
        public override string Path => Items[0].Item.Path;

        public override void Interact()
        {
            Tile.World.ContainerEvent(this);
        }
    }
}