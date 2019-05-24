using Godgame.Model.API;

namespace Godgame.Model.Structures
{
    class Pile : ItemContainerStructure
    {
        static Pile()
        {
            names[typeof(Pile)] = "Pile";
            AllMaxHitPoints[typeof(Pile)] = 20;
        }

        //TODO nem semmisül meg. Jól kezelné-e, ha sikerülne?
        public Pile(Tile tile, params ItemAmount[] items) : base(tile, items)
        {
            Inventory.Empty += () => { OnDestroyed(); };
        }

        //TODO miért lehet az Inventory null
        public override string ImagePath
        {
            get
            {
                if (Inventory == null)
                {
                    return "log.png";
                }
                return Inventory.Items[0].Item.ImagePath;
            }
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            if (Tile.Actor != null)
            {
                foreach (var items in Inventory.Items)
                {
                    Tile.Actor.Inventory.Add(items);
                }
            }
        }
    }
}
