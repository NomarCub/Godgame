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

        public override string Path => "log.png";
    }
}
