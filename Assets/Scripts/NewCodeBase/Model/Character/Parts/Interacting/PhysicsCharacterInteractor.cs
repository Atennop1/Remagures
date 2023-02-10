using System;
using Remagures.Model.Interactable;
using Remagures.View.Interactable;
using UnityEngine;

namespace Remagures.Model.Character
{
    public class PhysicsCharacterInteractor : MonoBehaviour
    {
        [SerializeField] private IContextClueView _contextClueView;
        private CharacterInteractor _characterInteractor;

        private int _countOfInteractablesInCollider;
        
        public void Construct(CharacterInteractor characterInteractor)
            => _characterInteractor = characterInteractor ?? throw new ArgumentNullException(nameof(characterInteractor));

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.gameObject.TryGetComponent(out IInteractable interactable))
                return;

            _contextClueView.DisplayQuestion();
            _countOfInteractablesInCollider++;
            
            if (_characterInteractor.CurrentInteractable == null)
                _characterInteractor.SetCurrentInteractable(interactable);
        }

        private void OnTriggerExit2D(Collider2D collider2d)
        {
            if (collider2d.gameObject.TryGetComponent<IInteractable>(out _))
                _countOfInteractablesInCollider--;
            
            if (_countOfInteractablesInCollider == 0)
                _contextClueView.UnDisplay();
        }

        private void Update()
        {
            if (_characterInteractor.CurrentInteractable is not { HasInteracted: true })
                return;
            
            _characterInteractor.EndInteraction();
            _contextClueView.UnDisplay();
            
            if (_countOfInteractablesInCollider != 0)
                _contextClueView.DisplayQuestion();
        }
    }
}