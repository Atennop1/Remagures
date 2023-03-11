using Remagures.Model.Health;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Listeners
{
    public sealed class AddHealthListenerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IItemFactory<IUsableItem> _usableItemFactory;
        [SerializeField] private IHealth _health;
        [SerializeField] private int _amountToAdd;

        private readonly SystemUpdate _systemUpdate = new();

        private void Awake()
        {
            var listener = new AddHealthListener(_usableItemFactory.Create(), _health, _amountToAdd);
            _systemUpdate.Add(listener);
        }

        private void Update()
            => _systemUpdate.UpdateAll();
    }
}