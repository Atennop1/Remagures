using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.Input
{
    public sealed class InteractingInput : SerializedMonoBehaviour, IInteractingInput
    {
        public bool HasInteracted { get; private set; }
        [SerializeField] private Button _button;

        private void Awake() 
            => _button.onClick.AddListener(OnClick);

        private void OnDestroy() 
            => _button.onClick.RemoveListener(OnClick);

        private void LateUpdate() 
            => HasInteracted = false;

        private void OnClick()
            => HasInteracted = true;
    }
}