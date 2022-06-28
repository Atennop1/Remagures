using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Dialogs/Database", order = 1)]
public class DialogDatabase : ScriptableObject
{
    [field: SerializeField] public string BaseNodeGUID { get; private set; }
    [field: SerializeField] public DialogContainer Container { get; private set; }

    [HideInInspector] public List<DialogNodeLinkData> Links;
    [HideInInspector] public string CurrentNodeGUID;
    [HideInInspector] public TextAsset CurrentDialog;
}