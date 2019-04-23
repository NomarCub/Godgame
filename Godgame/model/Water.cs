namespace Godgame.model
{
    class Water : Tile
    {
        public Water(Coordinate coordinate, World world) : base(coordinate, world) { }
        public override string Path => "water.png";
    }
}
