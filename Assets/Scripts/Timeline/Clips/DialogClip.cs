using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogClip : AssetClip<DialogBehaviour>
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<DialogBehaviour>.Create(graph, _template);
    }
}
