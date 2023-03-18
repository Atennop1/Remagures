using Remagures.Model.Interactable;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Root
{
    public sealed class InteractableWithInteractionEndByClickFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Button _endButton;
        private InteractableWithInteractionEndByClick _builtInteractable;
        
        public InteractableWithInteractionEndByClick Create()
        {
            if (_builtInteractable != null)
                return _builtInteractable;

            _builtInteractable = new InteractableWithInteractionEndByClick(_endButton);
            return _builtInteractable;
        }
    }
}