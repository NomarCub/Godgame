namespace Godgame.model
{
    abstract class Structure : IDrawable
    {
        Tile Tile;
        public abstract string Path { get; }
    }
}
