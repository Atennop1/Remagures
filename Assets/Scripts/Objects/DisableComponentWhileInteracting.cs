using UnityEngine;

public class DisableComponentWhileInteracting : MonoBehaviour
{
    [SerializeField] private PlayerInteracting _playerInteract;
    [SerializeField] private Behaviour _thisComponent;
    
    public void FixedUpdate()
    {
        if (_playerInteract.CurrentState == InteractingState.Interact)
            _thisComponent.enabled = false;
        else
            _thisComponent.enabled = true;
    }
}
