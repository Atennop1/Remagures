using System.Collections;
using System.Collections.Generic;
using Remagures.SO.PlayerStuff;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.QuestSystem
{
    public class QuestTextLabel : MonoBehaviour
    {
        [SerializeField] private Text _showingText;
        [SerializeField] private Animator _textAnimator;

        [SerializeField] private StringValue _currentString;

        public IReadOnlyCollection<string> LabelsQueue => _labelsQueue;
        private readonly Queue<string> _labelsQueue = new();

        private Coroutine _labelsCoroutine;
        private readonly int SHOW_ANIMATOR_NAME = Animator.StringToHash("Show");

        public void AddToQueueSignal()
        {
            AddToQueue(_currentString.Value);
        }
    
        public void AddToQueue(string text)
        {
            _labelsQueue.Enqueue(text);
            _labelsCoroutine ??= StartCoroutine(DisplayLabels());
        }

        private IEnumerator DisplayLabels()
        {
            while (_labelsQueue.Count > 0)
            {
                _showingText.text = _labelsQueue.Dequeue();

                _textAnimator.SetTrigger(SHOW_ANIMATOR_NAME);
                yield return new WaitForSeconds(3);
                _showingText.text = string.Empty;
            }
        
            _labelsCoroutine = null;
        }
    }
}
