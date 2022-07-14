using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private BoolValue _isPressed;

    [Space]
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Door _door;

    private bool _isActive;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            ActivateSwitch();
    }

    private void ActivateSwitch()
    {
        _isActive = true;
        _isPressed.Value = _isActive;
        _spriteRenderer.sprite = _activeSprite;
        _door.Interact();
    }
}
