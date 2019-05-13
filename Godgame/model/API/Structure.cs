using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Godgame.Model.API
{
    public abstract class Structure : Item
    {
        protected static IDictionary<Type, uint> AllMaxHitPoints = new Dictionary<Type, uint>();
        public uint HitPoints { get; protected set; }

        //returns true if destoryed by the hit
        public virtual bool GetHit(uint by)
        {
            Debug.WriteLine("hit");
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

        public Structure(Tile tile)
        {
            HitPoints = AllMaxHitPoints[this.GetType()];
            Tile = tile;
            tile.Structure = this;
        }

        public abstract void Interact();
    }
}
