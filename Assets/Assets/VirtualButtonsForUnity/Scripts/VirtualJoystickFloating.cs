using UnityEngine;

namespace Remagures.Assets.VirtualButtonsForUnity.Scripts
{
    public class VirtualJoystickFloating : VirtualJoystick
    {

        [SerializeField] private bool hideOnPointerUp = false;
        [SerializeField] private bool centralizeOnPointerUp = true;

        protected override void Awake()
        {
            joystickType = VirtualJoystickType.Floating;
            _hideOnPointerUp = hideOnPointerUp;
            _centralizeOnPointerUp = centralizeOnPointerUp;

            base.Awake();
        }

    }
}
