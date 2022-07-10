using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogChanger : MonoBehaviour
{
    [SerializeField] private DialogDatabase _npcDatabase;

    public void NextDialogGraph(int idOfLink)
    {
        DialogContainer container = _npcDatabase.Container;
        List<DialogNodeLinkData> linkDatas = new List<DialogNodeLinkData>();

        foreach (DialogNodeLinkData link in _npcDatabase.Container.NodeLinks)
            if (link.BaseNodeGUID == _npcDatabase.CurrentNodeGUID)
                linkDatas.Add(link);
        
        _npcDatabase.CurrentNodeGUID = linkDatas[idOfLink].TargetNodeGUID;
    }
}
