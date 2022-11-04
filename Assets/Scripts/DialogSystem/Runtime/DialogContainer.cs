using System.Collections.Generic;
using UnityEngine;

namespace Remagures.DialogSystem.Runtime
{
    [System.Serializable]
    public class DialogContainer : ScriptableObject
    {
        [NonReorderable] public List<DialogNodeLinkData> NodeLinks = new();
        [NonReorderable] public List<DialogNodeData> NodeData = new();
    }
}
