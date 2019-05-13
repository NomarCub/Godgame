using Godgame.Model.API;

namespace Godgame.Model.Structures
{
    class Pile : ItemContainerStructure
    {
        static Pile()
        {
            names[typeof(Pile)] = "Pile";
            AllMaxHitPoints[typeof(Pile)] = 110;
        }
        public Pile(Tile tile, params ItemAmount[] items) : base(tile, items) { }

        public override string Path => Items[0].Item.Path;
    }
}
