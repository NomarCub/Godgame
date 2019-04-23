namespace Godgame.model
{
    class Ground : Tile
    {
        public Ground(Coordinate coordinate, World world) : base(coordinate, world) { }
        public override string Path => "grass.png";
    }
}
