using Godgame.Model.API;

namespace Godgame.Model
{
    class Wood : Item
    {
        public override string Path => "log.png";

        static Wood() { names[typeof(Wood)] = "Wood"; }
    }
}
