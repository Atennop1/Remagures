using System;
using SaveSystem;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public sealed class Door : IInteractable
    {
        public bool HasInteractionEnded { get; private set; }
        
        [SerializeField] private DoorType _thisDoorType;
        [SerializeField] private BinaryStorage<int> _numberOfKeysStorage;

        [Space]
        [SerializeField] private SpriteRenderer _doorSprite;
        [SerializeField] private BoxCollider2D _collider;

        public void Interact()
        {
            switch (_thisDoorType)
            {
                case DoorType.Key when _numberOfKeysStorage.Load() > 0:
                    _numberOfKeysStorage.Save(_numberOfKeysStorage.Load() - 1);
                    break;
                
                case DoorType.Key:
                    return;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _doorSprite.enabled = false;
            _collider.enabled = false;
            HasInteractionEnded = true;
            _collider.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        
        public void CloseDoor()
        {
            _doorSprite.enabled = true;
            _collider.enabled = true;
            _collider.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        public void OnInteractionEnd() { }
    }
}
