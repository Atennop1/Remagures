using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class UnityButton : SerializedMonoBehaviour
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