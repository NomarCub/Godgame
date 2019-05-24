using System.Collections.ObjectModel;

namespace Godgame.Model.API
{

    //TODO ItemContainerStructure és Actorba belerakni
    public class Inventory
    {
        public delegate void EmptyEvent();

        public EmptyEvent Empty;

        private readonly ObservableCollection<ItemAmount> _Items;
        public ReadOnlyObservableCollection<ItemAmount> Items { get; }

        public Inventory(params ItemAmount[] items)
        {
            _Items = new ObservableCollection<ItemAmount>();
            foreach (var item in items)
                Add(item);
            Items = new ReadOnlyObservableCollection<ItemAmount>(_Items);
        }

        public void Add(ItemAmount newItems)
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

        public void Remove(ItemAmount itemAmount)
        {
            _Items.Remove(itemAmount);
            if (_Items.Count == 0)
                Empty?.Invoke();
        }

        public void Remove(int index)
        {
            _Items.RemoveAt(index);
        }
    }
}
