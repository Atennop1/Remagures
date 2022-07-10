using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogContainer : ScriptableObject
{
    [NonReorderable] public List<DialogNodeLinkData> NodeLinks = new List<DialogNodeLinkData>();
    [NonReorderable] public List<DialogNodeData> NodeData = new List<DialogNodeData>();
}
