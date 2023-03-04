using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class RuneItemFactory : SerializedMonoBehaviour, IItemFactory<IRuneItem>
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private IRuneFactory _runeFactory;
        
        private IRuneItem _builtItem;
        
        public IRuneItem Create()
        {
            if (_builtItem != null)
                return _builtItem;

            var item = new Item(_data.Name, _data.Description, _data.Sprite, _data.IsStackable);
            _builtItem = new RuneItem(item, _runeFactory.Create());
            return _builtItem;
        }
    }
}