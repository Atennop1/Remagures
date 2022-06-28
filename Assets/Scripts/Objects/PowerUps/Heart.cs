using UnityEngine;

public class Heart : PowerUp
{
    [SerializeField] private FloatValue _playerHealth;
    [SerializeField] private FloatValue _heartContainers;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            _playerHealth.Value += 4;
            if (_playerHealth.Value > _heartContainers.Value * 4)
                _playerHealth.Value = _heartContainers.Value * 4;
            _powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
