using Remagures.Model.Interactable;
using Remagures.View.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public class ChestWithViewFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private IChestFactory _chestFactory;
        [SerializeField] private IChestView _chestView;
        private IChest _builtChest;

        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;

            _builtChest = new ChestWithView(_chestFactory.Create(), _chestView);
            return _builtChest;
        }
    }
}