using Remagures.Model.Interactable;
using Remagures.Root.Dialogs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestWithDialogFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private IChestFactory _chestFactory;
        [SerializeField] private DialogPlayerFactory _dialogPlayerFactory;
        [SerializeField] private DialogFactory _dialogFactory;
        private IChest _builtChest;
        
        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;

            _builtChest = new ChestWithDialog(_chestFactory.Create(), _dialogPlayerFactory.Create(), _dialogFactory.Create());
            return _builtChest;
        }
    }
}