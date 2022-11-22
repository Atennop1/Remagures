using System.Collections;
using Remagures.DialogSystem.UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Remagures.Timeline.Behaviours
{
    [System.Serializable]
    public class DialogBehaviour : PlayableBehaviour
    {
        [SerializeField] [TextArea] private string _text;
        [FormerlySerializedAs("_disableAferReplica")] [SerializeField] private bool _disableAfterReplica;

        private bool _beenStopped;
        private DialogTypeWriter _writer;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (_beenStopped) 
                return;
        
            TimelineView.Instance.Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            _writer = playerData as DialogTypeWriter;
            
            if (_writer != null) _writer.StartCoroutine(DisplayTextCoroutine());
            _beenStopped = true;
        }   

        public IEnumerator DisplayTextCoroutine()
        {
            _writer.View.DialogWindow.GetComponent<Button>().onClick.AddListener(Tap);
            _writer.View.DialogWindow.SetActive(true);

            TimelineView.Instance.SetCanContinue(false);
            yield return new WaitForSeconds(999999999);

            if (_disableAfterReplica)
                Tap();
        }

        public void Tap()
        {
            _writer.Tap();
            TimelineView.Instance.SetCanContinue(_disableAfterReplica);

            if (!_disableAfterReplica)
                TimelineView.Instance.Director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            
            _writer.View.DialogWindow.GetComponent<Button>().onClick.RemoveListener(Tap);
            _writer.View.ContinueText.text = "Нажмите, чтобы продолжить";
        }
    }
}
