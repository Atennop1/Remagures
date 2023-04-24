using Remagures.Model.QuestSystem;
using SaveSystem;
using SaveSystem.Paths;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ProgressWithSavingFactory : IProgressFactory
    {
        [SerializeField] private IProgressFactory _progressFactory;
        [SerializeField] private string _savePath;
        private IProgress _builtProgress;
        
        public IProgress Create()
        {
            if (_builtProgress != null)
                return _builtProgress;

            var storage = new BinaryStorage<int>(new Path(_savePath));
            _builtProgress = new ProgressWithSaving(_progressFactory.Create(), storage);
            return _builtProgress;
        }
    }
}