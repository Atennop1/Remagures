using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Root;
using Remagures.Tools;

namespace Remagures.Model.InventorySystem
{
    public sealed class ItemsList<TItem> : IItemsList<TItem> where TItem: IItem
    {
        private readonly List<IItemFactory<TItem>> _factories;

        public ItemsList(List<IItemFactory<TItem>> factories) 
            => _factories = factories ?? throw new ArgumentNullException(nameof(factories));

        public int GetItemID(TItem item)
        {
            if (!_factories.Any(factory => factory.Create().AreEquals(item)))
                throw new InvalidOperationException("Unknown item");

            return _factories.Find(factory => factory.Create().AreEquals(item)).ItemID;
        }

        public TItem GetByID(int id)
        {
            if (_factories.All(factory => factory.ItemID != id))
                throw new InvalidOperationException("Unknown id");

            return _factories.Find(factory => factory.ItemID == id).Create();
        }
    }
}