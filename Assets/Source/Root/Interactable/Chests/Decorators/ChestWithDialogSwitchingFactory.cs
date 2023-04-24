using Remagures.Model.Interactable;
using Remagures.Root.Dialogs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestWithDialogSwitchingFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private IChestFactory _chestFactory;
        [SerializeField] private DialogSwitcherFactory _dialogSwitcherFactory;
        private IChest _builtChest;
        
        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;

            _builtChest = new ChestWithDialogSwitching(_chestFactory.Create(), _dialogSwitcherFactory.Create());
            return _builtChest;
        }
    }
}