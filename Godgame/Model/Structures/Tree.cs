using Godgame.Model.API;
using Godgame.Model.Items;

namespace Godgame.Model.Structures
{
    class Tree : Structure
    {
        public override string Path => "tree.png";

        static Tree()
        {
            names[typeof(Tree)] = "Tree";
            AllMaxHitPoints[typeof(Tree)] = 50;
        }

        public Tree(Tile tile) : base(tile) { }

        override public bool GetHit(uint by)
        {
            bool ret = base.GetHit(by);
            if (ret)
            {
                var pile = new Pile(Tile);
                pile.ReceiveItemAmount((new Wood(), 5));
                Tile.Structure = pile;
            }
            return ret;
        }

        public override void Interact() { }
    }
}
