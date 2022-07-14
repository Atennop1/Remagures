using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[System.Serializable]
public abstract class AssetClip<T> : PlayableAsset, ITimelineClipAsset where T: PlayableBehaviour, new()
{
    [SerializeField] protected T _template = new T();
    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) { return new Playable(); }
}
