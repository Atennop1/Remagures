using System;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class UnityButton : MonoBehaviour
    {
        private IButton _button;
        private Button _unityButton;
        
        public void Construct(IButton button)
        {
            _button = button ?? throw new ArgumentNullException(nameof(button));
            _unityButton = GetComponent<Button>();
            _unityButton.onClick.AddListener(button.Press);
        }

        private void OnDestroy() 
            => _unityButton.onClick.RemoveListener(_button.Press);
    }
}