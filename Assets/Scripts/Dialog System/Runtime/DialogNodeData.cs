using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogNodeData
{
    public string GUID;
    public string nodeName;
    public Dialog dialog;
    public Vector2 position;
    public List<DialogNodeLinkData> linkList = new List<DialogNodeLinkData>();
}
