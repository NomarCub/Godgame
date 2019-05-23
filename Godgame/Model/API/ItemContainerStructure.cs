using System.Collections.ObjectModel;

namespace Godgame.Model.API
{
    public abstract class ItemContainerStructure : Structure
    {
        public Inventory Inventory { get; }
        public ItemContainerStructure(Tile tile, params ItemAmount[] items) : base(tile)
        {
            Inventory = new Inventory(items);
        }

        public override void Interact()
        {
            Tile.World.ContainerEvent(this);
        }
    }
}