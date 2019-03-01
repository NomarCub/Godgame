namespace Godgame.model
{
    abstract class Structure : IDrawable
    {
        Tile CurrentTile;
        public abstract string Path { get; }
    }
}
