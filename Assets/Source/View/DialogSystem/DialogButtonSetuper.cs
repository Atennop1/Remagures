using Remagures.Model.DialogSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.DialogSystem
{
    public class DialogButtonSetuper : MonoBehaviour
    {
        [SerializeField] private Button _dialogButton;

        public void Setup(IDialogPlayer dialogPlayer)
            => _dialogButton.onClick.AddListener(dialogPlayer.ContinueDialog);
    }
}