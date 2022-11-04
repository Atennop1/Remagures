using System.Linq;
using Remagures.DialogSystem.Core;
using UnityEngine;

namespace Remagures.DialogSystem.Reactions
{
    public class DialogChanger : MonoBehaviour
    {
        [SerializeField] private DialogDatabase _npcDatabase;

        public void NextDialogGraph(int idOfLink)
        {
            var linkData = _npcDatabase.Container.NodeLinks.Where(link => link.BaseNodeGUID == _npcDatabase.CurrentNodeGUID).ToList();
            _npcDatabase.CurrentNodeGUID = linkData[idOfLink].TargetNodeGUID;
        }
    }
}
