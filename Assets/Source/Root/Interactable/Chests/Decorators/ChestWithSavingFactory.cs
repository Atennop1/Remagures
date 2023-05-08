using Remagures.Model.Interactable;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestWithSavingFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private string _name;
        [SerializeField] private IChestFactory _chestFactory;
        private IChest _builtChest;
        
        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;
            
            _builtChest = new ChestWithSaving(_chestFactory.Create(), new BinaryStorage<bool>(new Path($"/Chests/{_name}")));
            return _builtChest;
        }
    }
}