using Remagures.Model.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ChestWithGoalCompletingFactory : SerializedMonoBehaviour, IChestFactory
    {
        [SerializeField] private IChestFactory _chestFactory;
        [SerializeField] private GoalFactory _goalFactory;
        private IChest _builtChest;
        
        public IChest Create()
        {
            if (_builtChest != null)
                return _builtChest;

            _builtChest = new ChestWithGoalCompleting(_chestFactory.Create(), _goalFactory.Create());
            return _builtChest;
        }
    }
}