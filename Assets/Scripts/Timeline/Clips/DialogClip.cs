using Remagures.Timeline.Behaviours;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Remagures.Timeline.Clips
{
    [System.Serializable]
    public class DialogClip : AssetClip<DialogBehaviour>
    {
        public new ClipCaps clipCaps => ClipCaps.All;
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<DialogBehaviour>.Create(graph, _template);
        }
    }
}
