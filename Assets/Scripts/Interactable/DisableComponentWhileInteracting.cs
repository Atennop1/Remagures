using Remagures.Player.Components;
using Remagures.Timeline;
using UnityEngine;

namespace Remagures.Interactable
{
    public class DisableComponentWhileInteracting : MonoBehaviour
    {
        [SerializeField] private PlayerInteractingHandler _playerInteract;
        [SerializeField] private Behaviour _thisComponent;
    
        public void FixedUpdate()
        {
            var isCutscenePlaying = TimelineView.Instance != null && TimelineView.Instance.IsPlaying;
            _thisComponent.enabled = _playerInteract.CurrentState != InteractingState.Interact && !isCutscenePlaying;
        }
    }
}
