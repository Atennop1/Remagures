using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Remagures.View.QuestSystem;

namespace Remagures.Model.QuestSystem
{
    public sealed class QuestPopups : IQuestPopups
    {
        private readonly Queue<string> _popupsQueue = new();
        private readonly IQuestPopupView _view;
        private UniTask _playingTask;

        public QuestPopups(IQuestPopupView view)
            => _view = view ?? throw new ArgumentNullException(nameof(view));

        public async void AddTextToQueue(string text)
        {
            if (_popupsQueue.Contains(text))
                return;

            _popupsQueue.Enqueue(text);

            if (_playingTask.Status is UniTaskStatus.Pending) 
                return;
            
            _playingTask = PlayPopups();
            await _playingTask;
        }

        private async UniTask PlayPopups()
        {
            while (_popupsQueue.Count > 0)
            {
                _view.Display(_popupsQueue.Dequeue());
                await UniTask.Delay(3000);
            }
        }
    }
}