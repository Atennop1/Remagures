using Remagures.Model.Character;
using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public sealed class Switch : SerializedMonoBehaviour
    {
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Door _door;
        
        private bool _isActive;
        private SpriteRenderer _spriteRenderer;
        private BinaryStorage<bool> _storage;

        private void Start()
            => _spriteRenderer = GetComponent<SpriteRenderer>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PhysicsCharacter _))
                ActivateSwitch();
        }

        private void ActivateSwitch()
        {
            _isActive = !_storage.HasSave() || _storage.Load();
            _spriteRenderer.sprite = _activeSprite;
            _door.Interact();
        }

        private void OnDisable()
            => _storage.Save(_isActive);
    }
}
