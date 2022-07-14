using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogBehaviour : PlayableBehaviour
{
    [SerializeField] [TextArea] private string _text;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TimelineView.Instance.Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        DialogTypeWritter writter = playerData as DialogTypeWritter;
        writter.StartTyping(_text);
    }   
}
