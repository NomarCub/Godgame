namespace Godgame.model
{
    abstract class Actor : IDrawable
    {
        public Tile CurrentTile { get; set; }
        public abstract string Path { get; }
    }
}
