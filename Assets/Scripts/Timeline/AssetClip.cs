using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public class AssetClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private AnimatorMixerBehaviour _template = new AnimatorMixerBehaviour();
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<AnimatorMixerBehaviour>.Create(graph, _template);
    }
}
