using System;
using System.Collections.Generic;

namespace Godgame.Model.API
{
    public abstract class Item : IDrawable
    {
        public abstract string Path { get; }
        protected static IDictionary<Type, string> names = new Dictionary<Type, string>();
        public string Name
        {
            get { return names[this.GetType()]; }
        }
    }

    public class ItemAmount : IEquatable<ItemAmount>
    {

        public Item Item { get; set; }
        public uint Amount { get; set; }
        public ItemAmount(Item item, uint amount)
        {
            Item = item;
            Amount = amount;
        }

        public static implicit operator ItemAmount((Item Item, uint Amount) tuple)
        {
            return new ItemAmount(tuple.Item, tuple.Amount);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemAmount);
        }

        public bool Equals(ItemAmount other)
        {
            return other != null &&
                   EqualityComparer<Item>.Default.Equals(Item, other.Item) &&
                   Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item, Amount);
        }

        public static bool operator ==(ItemAmount amount1, ItemAmount amount2)
        {
            return EqualityComparer<ItemAmount>.Default.Equals(amount1, amount2);
        }

        public static bool operator !=(ItemAmount amount1, ItemAmount amount2)
        {
            return !(amount1 == amount2);
        }
    }
}
