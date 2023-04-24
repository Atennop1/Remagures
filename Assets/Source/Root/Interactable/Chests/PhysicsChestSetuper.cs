using Remagures.Model.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsChestSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IChestFactory _chestFactory;
        [SerializeField] private PhysicsChest _physicsChest;

        private void Awake()
            => _physicsChest.Construct(_chestFactory.Create());
    }
}