using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.Input
{
    public sealed class AttackInput : SerializedMonoBehaviour, IAttackInput
    {
        public bool HasAttacked { get; private set; }
        [SerializeField] private Button _button;

        private void Awake() 
            => _button.onClick.AddListener(OnClick);

        private void OnDestroy() 
            => _button.onClick.RemoveListener(OnClick);

        private void LateUpdate() 
            => HasAttacked = false;

        private void OnClick()
            => HasAttacked = true;
    }
}