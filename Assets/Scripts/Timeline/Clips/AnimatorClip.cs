using Remagures.Timeline.Behaviours;
using UnityEngine;
using UnityEngine.Playables;

namespace Remagures.Timeline.Clips
{
    [System.Serializable]
    public class AnimatorClip : AssetClip<RuntimeAnimatingBehaviour>
    {
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<RuntimeAnimatingBehaviour>.Create(graph, _template);
        }
    }
}
