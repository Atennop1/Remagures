using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextLabel : MonoBehaviour
{
    [SerializeField] private Text _showingText;
    [SerializeField] private Animator _textAnimator;

    [SerializeField] private StringValue _currentString;

    public IReadOnlyCollection<string> LabelsQueue => _labelsQueue;
    private Queue<string> _labelsQueue = new Queue<string>();

    private Coroutine _labelsCoroutine;

    public void AddToQueueSignal()
    {
        AddToQueue(_currentString.Value);
    }
    
    public void AddToQueue(string text)
    {
        _labelsQueue.Enqueue(text);
        if (_labelsCoroutine == null)
            _labelsCoroutine = StartCoroutine(DisplayLabels());
    }

    public IEnumerator DisplayLabels()
    {
        while (_labelsQueue.Count > 0)
        {
            _showingText.text = _labelsQueue.Dequeue();

            _textAnimator.SetTrigger("Show");
            yield return new WaitForSeconds(3);
            _showingText.text = string.Empty;
        }
        
        _labelsCoroutine = null;
    }
}
