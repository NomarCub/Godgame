using System.Collections.ObjectModel;

namespace Godgame.Model.API
{
    public abstract class ItemContainerStructure : Structure
    {
        public ItemContainerStructure(Tile tile, params ItemAmount[] items) : base(tile)
        {
            _Items = new ObservableCollection<ItemAmount>();
            foreach (var item in items)
                ReceiveItemAmount(item);
            Items = new ReadOnlyObservableCollection<ItemAmount>(_Items);
        }

        public void ReceiveItemAmount(ItemAmount newItems)
        {
            foreach (var oldItem in _Items)
            {
                if (oldItem.Item.GetType() == newItems.Item.GetType())
                {
                    oldItem.Amount += newItems.Amount;
                    _Items.Remove(oldItem);
                    _Items.Add(oldItem);
                    return;
                }
            }
            _Items.Add(newItems);
        }

        private readonly ObservableCollection<ItemAmount> _Items;
        public ReadOnlyObservableCollection<ItemAmount> Items { get; }

        public override void Interact()
        {
            Tile.World.ContainerEvent(this);
        }
    }
}