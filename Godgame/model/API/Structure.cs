using System.Threading.Tasks;

namespace Godgame.Model.API
{
    public abstract class Structure : Item
    {
        public uint HitPoints { get; private set; }

        //returns true if destoryed by the hit
        public virtual bool GetHit(uint by)
        {
            if (by < HitPoints)
            {
                HitPoints -= by;
                return false;
            }
            else
            {
                Tile.Structure = null;
                return true;
            }
        }

        private Tile _Tile;
        public Tile Tile
        {
            get { return _Tile; }
            set
            {
                if (_Tile != value)
                {
                    _Tile = value;
                    _Tile.Structure = this;
                }
            }
        }

        public Structure(uint HP, Tile tile)
        {
            HitPoints = HP;
            Tile = tile;
            tile.Structure = this;
        }

        public abstract Task Interact();
    }
}
