using Godgame.Model.API;
using Godgame.Model.Items;

namespace Godgame.Model.Structures
{
    class Tree : Structure
    {
        public override string ImagePath => "tree.png";

        static Tree()
        {
            names[typeof(Tree)] = "Tree";
            AllMaxHitPoints[typeof(Tree)] = 50;
        }

        public Tree(Tile tile) : base(tile) { }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            var pile = new Pile(Tile, new ItemAmount(new Wood(), 5));
            Tile.Structure = pile;
        }

        public override void Interact() { }
    }
}
