using UnityEngine;

public enum DoorType
{
    Key,
    Enemy,
    Default
}

public class Door : Interactable
{
    [SerializeField] private DoorType _thisDoorType;
    [SerializeField] private FloatValue _numberOfKeys;

    [Space]
    [SerializeField] private SpriteRenderer _doorSprite;
    [SerializeField] private BoxCollider2D _collider;

    public override void Interact()
    {
        if (_thisDoorType == DoorType.Key && _numberOfKeys.Value > 0)
            _numberOfKeys.Value--;
        else if (_thisDoorType == DoorType.Key)
            return;

        _doorSprite.enabled = false;
        _collider.enabled = false;
        _collider.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        if (PlayerInteract == this)
            PlayerInteract.ResetCurrentState(this);
    }
    
    public void CloseDoor()
    {
        _doorSprite.enabled = true;
        _collider.enabled = true;
        _collider.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
