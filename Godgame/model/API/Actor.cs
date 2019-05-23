using System.Collections.ObjectModel;

namespace Godgame.Model.API
{
    public abstract class Actor : IDrawable
    {
        private Tile _CurrentTile;

        public readonly uint BaseAttack = 10;

        public virtual uint AttackPower { get => BaseAttack; }
        public Tile CurrentTile
        {
            get => _CurrentTile;
            set
            {
                if (value != _CurrentTile)
                {
                    if (_CurrentTile != null)
                        _CurrentTile.Actor = null;
                    _CurrentTile = value;
                    if (_CurrentTile != null)
                    {
                        _CurrentTile.Actor = this;
                    }
                }
            }

        }

        private readonly ObservableCollection<ItemAmount> _Inventory = new ObservableCollection<ItemAmount>();
        public ReadOnlyObservableCollection<ItemAmount> Inventory { get; }
        public abstract string ImagePath { get; }


        public Actor()
        {
            _Inventory = new ObservableCollection<ItemAmount>();
            Inventory = new ReadOnlyObservableCollection<ItemAmount>(_Inventory);
        }

        public void ReceiveItemAmount(ItemAmount newItems)
        {
            foreach (var oldItem in _Inventory)
            {
                if (oldItem.Item.GetType() == newItems.Item.GetType())
                {
                    oldItem.Amount += newItems.Amount;
                    _Inventory.Remove(oldItem);
                    _Inventory.Add(oldItem);
                    return;
                }
            }
            _Inventory.Add(newItems);
        }

        public void Hit()
        {
            var structure = CurrentTile.Structure;
            structure?.GetHit(AttackPower);
        }
    }
}
