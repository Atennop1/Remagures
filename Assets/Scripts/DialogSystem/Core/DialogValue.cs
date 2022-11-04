using UnityEngine;

namespace Remagures.DialogSystem.Core
{
    [CreateAssetMenu(fileName = "New DialogValue", menuName = "Dialog System/DialogValue", order = 0)]
    public class DialogValue : ScriptableObject 
    {
        public Dialog ThisDialog;
        public DialogDatabase NPCDatabase;
    }
}
