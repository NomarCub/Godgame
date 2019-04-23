namespace Godgame.model
{
    class Tree : Structure
    {
        public override string Path => "tree.png";

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

        public Tree(Tile tile) : base(300, tile) { }
    }
}
