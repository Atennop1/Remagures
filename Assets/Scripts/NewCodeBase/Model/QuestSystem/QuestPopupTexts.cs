using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Remagures.View.QuestSystem;

namespace Remagures.Model.QuestSystem
{
    public sealed class QuestPopupTexts
    {
        private readonly Queue<string> _popupsQueue = new();
        private readonly QuestPopupTextView _view;
        private UniTask _playingTask;
        
        public async void AddTextToPopupQueue(string text)
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
                _view.DisplayText(_popupsQueue.Dequeue());
                await UniTask.Delay(3000);
            }
        }
    }
}