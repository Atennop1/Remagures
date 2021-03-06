using UnityEngine;

public class Coin : PowerUp
{
    [SerializeField] private FloatValue _numberOfCoins;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && !collision.isTrigger)
        {
            _numberOfCoins.Value++;
            PowerUpSignal.Invoke();
            Destroy(gameObject);
        }
    }
}
