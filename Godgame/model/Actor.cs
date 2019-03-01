namespace Godgame.model
{
    abstract class Actor : IDrawable
    {
        Tile CurrentTile;
        public abstract string Path { get; }
    }
}
