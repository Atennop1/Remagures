using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogNode : Node
{
    public string GUID;
    public string nodeName;
    public TextAsset DialogueAsset;
    public bool EntryPoint = false;
}
