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
        public Pile(Tile tile, params ItemAmount[] items) : base(tile, items) { }

        //TODO miért lehet az Items null
        public override string ImagePath
        {
            get
            {
                if (Items == null || Items.Count == 0)
                {
                    return "void.png";
                }
                return Items[0].Item.ImagePath;
            }
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            if (Tile.Actor != null)
            {
                foreach (var items in Items)
                {
                    Tile.Actor.ReceiveItemAmount(items);
                }
            }
        }
    }
}
