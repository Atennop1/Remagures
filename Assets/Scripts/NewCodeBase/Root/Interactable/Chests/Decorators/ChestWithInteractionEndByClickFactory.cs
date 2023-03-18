using Remagures.Model.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestWithInteractionEndByClickFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private IChestFactory _chestFactory;
        [SerializeField] private InteractableWithInteractionEndByClickFactory _interactableWithInteractionEndByClickFactory;
        private IChest _builtChest;
        
        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;
            
            _builtChest = new ChestWithInteractionEndByClick(_chestFactory.Create(), _interactableWithInteractionEndByClickFactory.Create());
            return _builtChest;
        }
    }
}