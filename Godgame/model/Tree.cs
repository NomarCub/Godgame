using Godgame.Model.API;
using System.Threading.Tasks;

namespace Godgame.Model
{
    class Tree : Structure
    {
        public override string Path => "tree.png";

        public Tree(Tile tile) : base(50, tile) { }

        static Tree() { names[typeof(Tree)] = "Tree"; }
        override public bool GetHit(uint by)
        {
            bool ret = base.GetHit(by);
            if (ret)
            {
                var chest = new Chest(Tile);
                chest.items.Add((new Wood(), 5));
                Tile.Structure = chest;

            }
            return ret;
        }


        public async override Task Interact() { }
    }
}
