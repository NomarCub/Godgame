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

        public Inventory Inventory { get; } = new Inventory();

        public abstract string ImagePath { get; }

        public void Hit()
        {
            var structure = CurrentTile.Structure;
            structure?.GetHit(AttackPower);
        }
    }
}
