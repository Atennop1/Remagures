using System.Collections.Generic;
using Remagures.DialogSystem.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.DialogSystem.Runtime
{
    [System.Serializable]
    public class DialogNodeData
    {
        public string GUID;
        [FormerlySerializedAs("nodeName")] public string Name;
    
        [FormerlySerializedAs("dialog")] public Dialog Dialog;
        [FormerlySerializedAs("position")] public Vector2 Position;
        [FormerlySerializedAs("linkList")] public List<DialogNodeLinkData> LinkList = new();
    }
}
