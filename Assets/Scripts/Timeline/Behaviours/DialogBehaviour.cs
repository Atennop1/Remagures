using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class DialogBehaviour : PlayableBehaviour
{
    [SerializeField] [TextArea] private string _text;
    [SerializeField] private bool _disableAferReplica;

    private bool _beenStoped;
    private DialogTypeWritter _writter;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!_beenStoped)
        {
            TimelineView.Instance.Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            _writter = playerData as DialogTypeWritter;
            _writter.StartCoroutine(DisplayTextCoroutine());
            _beenStoped = true;
        }
    }   

    public IEnumerator DisplayTextCoroutine()
    {
        _writter.View.DialogWindow.GetComponent<Button>().onClick.AddListener(Tap);
        _writter.View.DialogWindow.SetActive(true);
        _writter.View.ContinueText.text = "Нажмите, чтобы пролистать";

        TimelineView.Instance.SetCanContinue(false);
        yield return _writter.StartCoroutine(_writter.Type(_text));

        if (_disableAferReplica)
            Tap();
    }

    public void Tap()
    {
        _writter.Tap();
        TimelineView.Instance.SetCanContinue(_disableAferReplica);

        if (!_disableAferReplica)
            TimelineView.Instance.Director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            
        _writter.View.DialogWindow.GetComponent<Button>().onClick.RemoveListener(Tap);
        _writter.View.ContinueText.text = "Нажмите, чтобы продолжить";
    }
}
