using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class AnimatorClip : AssetClip<RuntimeAnimatingBehaviour>
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<RuntimeAnimatingBehaviour>.Create(graph, _template);
    }
}
