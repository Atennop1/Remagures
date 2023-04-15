using System;
using SaveSystem;

namespace Remagures.Model.QuestSystem
{
    public sealed class ProgressWithSaving : IProgress
    {
        public int CurrentPoints => _progress.CurrentPoints;
        public int RequiredPoints => _progress.RequiredPoints;
        
        private readonly IProgress _progress;
        private readonly ISaveStorage<int> _saveStorage;

        public ProgressWithSaving(IProgress progress, ISaveStorage<int> saveStorage)
        {
            _progress = progress ?? throw new ArgumentNullException(nameof(progress));
            _saveStorage = saveStorage ?? throw new ArgumentNullException(nameof(saveStorage));
            
            if (_saveStorage.HasSave())
                _progress.AddPoints(_saveStorage.Load());
        }

        public void AddPoints(int amount)
        {
            _progress.AddPoints(amount);
            _saveStorage.Save(CurrentPoints);
        }
    }
}