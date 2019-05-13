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
                    _CurrentTile = value;
                    if (_CurrentTile != null) _CurrentTile.Actor = this;
                }
            }

        }

        private readonly ObservableCollection<ItemAmount> _Inventory = new ObservableCollection<ItemAmount>();
        public ReadOnlyObservableCollection<ItemAmount> Inventory { get; }
        public abstract string Path { get; }


        public Actor()
        {
            _Inventory = new ObservableCollection<ItemAmount>();
            Inventory = new ReadOnlyObservableCollection<ItemAmount>(_Inventory);
        }

        public void ReceiveItemAmount(ItemAmount items)
        {
            foreach (var item in _Inventory)
            {
                if (item.Item == items.Item)
                {
                    item.Amount += items.Amount;
                    return;
                }
            }
            _Inventory.Add(items);
        }

        public void Hit()
        {
            var structure = CurrentTile.Structure;
            structure?.GetHit(AttackPower);
        }
    }
}
