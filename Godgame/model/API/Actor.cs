using System.Collections.Generic;

namespace Godgame.model
{
    abstract class Actor : IDrawable
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

        public IList<Item> Items = new List<Item>();
        public abstract string Path { get; }

        public void Hit()
        {
            var structure = CurrentTile.Structure;
            structure?.GetHit(AttackPower);
        }
    }
}
