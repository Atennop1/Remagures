using UnityEngine;

[CreateAssetMenu(fileName = "New DialogValue", menuName = "Dialogs/DialogValue", order = 0)]
public class DialogValue : ScriptableObject 
{
    public TextAsset ThisDialog;
    public DialogDatabase NPCDatabase;
}
