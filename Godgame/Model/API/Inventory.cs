using System.Collections.ObjectModel;

namespace Godgame.Model.API
{

    //TODO ItemContainerStructure és Actorba belerakni
    class Inventory : ObservableCollection<ItemAmount>
    {
        public new void Add(ItemAmount a)
        {
            base.Add(a);
        }
    }
}
