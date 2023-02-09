using Remagures.Tools.SwampAttack.Runtime.Tools.SaveSystem;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public class Switch : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Door _door;
        
        private SpriteRenderer _spriteRenderer;
        private bool _isActive;
        private BinaryStorage _storage;

        private void Start()
            => _spriteRenderer = GetComponent<SpriteRenderer>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player _))
                ActivateSwitch();
        }

        private void ActivateSwitch()
        {
            _isActive = !_storage.Exist(_name) || _storage.Load<bool>(_name);
            _spriteRenderer.sprite = _activeSprite;
            _door.Interact();
        }

        private void OnDisable()
            => _storage.Save(_isActive, _name);
    }
}
