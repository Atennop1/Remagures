using System;
using Remagures.SO.PlayerStuff;
using UnityEngine;

namespace Remagures.Interactable
{
    public class Door : Remagures.Interactable.Abstraction.Interactable
    {
        [SerializeField] private DoorType _thisDoorType;
        [SerializeField] private FloatValue _numberOfKeys;

        [Space]
        [SerializeField] private SpriteRenderer _doorSprite;
        [SerializeField] private BoxCollider2D _collider;

        public override void Interact()
        {
            switch (_thisDoorType)
            {
                case DoorType.Key when _numberOfKeys.Value > 0:
                    _numberOfKeys.Value--;
                    break;
                case DoorType.Key:
                    return;
            
                case DoorType.Enemy:
                case DoorType.Default:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _doorSprite.enabled = false;
            _collider.enabled = false;
            _collider.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
            PlayerInteract.ResetCurrentInteractable(this);
        }
    
        public void CloseDoor()
        {
            _doorSprite.enabled = true;
            _collider.enabled = true;
            _collider.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
