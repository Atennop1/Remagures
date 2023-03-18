using Remagures.Model.Interactable;
using Remagures.View.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestWithItemRaisingFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private IChestFactory _chestFactory;
        [SerializeField] private IChestWithItemRaisingView _chestWithItemRaisingView;
        private IChest _builtChest;
        
        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;

            _builtChest = new ChestWithItemRaising(_chestFactory.Create(), _chestWithItemRaisingView);
            return _builtChest;
        }
    }
}