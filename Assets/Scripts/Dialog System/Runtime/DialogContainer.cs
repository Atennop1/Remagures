using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogContainer : ScriptableObject
{
    public List<DialogNodeLinkData> NodeLinks = new List<DialogNodeLinkData>();
    public List<DialogNodeData> NodeData = new List<DialogNodeData>();
}
