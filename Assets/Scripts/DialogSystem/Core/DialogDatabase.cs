using System;
using System.Collections.Generic;
using Remagures.DialogSystem.Runtime;
using UnityEngine;

namespace Remagures.DialogSystem.Core
{
    [CreateAssetMenu(fileName = "New Database", menuName = "Dialog System/Database", order = 1)]
    public class DialogDatabase : ScriptableObject
    {
        [field: SerializeField] public string BaseNodeGUID { get; private set; }
        [field: SerializeField] public DialogContainer Container { get; private set; }

        [HideInInspector] public string CurrentNodeGUID;
        public Dialog CurrentDialog => Container.NodeData.Find(x => x.GUID == CurrentNodeGUID).Dialog;
        
    }
}