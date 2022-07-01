using UnityEngine;

public class HeartAdder : PowerUp
{
    [SerializeField] private FloatValue _heartContainers;
    [SerializeField] private FloatValue _maxHearts;
    [SerializeField] private FloatValue _playerHealth;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger)
        {
            if (_heartContainers.Value < _maxHearts.Value)
                _heartContainers.Value++;
                
            _playerHealth.Value = _heartContainers.Value * 4;
            PowerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
