using Remagures.Model.Buttons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UnityButtonSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IButtonFactory _buttonFactory;
        [SerializeField] private UnityButton _unityButton;

        private void Awake() 
            => _unityButton.Construct(_buttonFactory.Create());
    }
}