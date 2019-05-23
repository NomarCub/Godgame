using Godgame.Model.API;

namespace Godgame.Model.Structures
{
    class Chest : ItemContainerStructure
    {
        static Chest()
        {
            names[typeof(Chest)] = "Chest";
            AllMaxHitPoints[typeof(Chest)] = 100;
        }
        public Chest(Tile tile, params ItemAmount[] items) : base(tile, items) { }

        public override string ImagePath => "chest.png";
    }
}
