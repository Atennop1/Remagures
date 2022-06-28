using UnityEngine;

public class Coin : PowerUp
{
    [SerializeField] private FloatValue _numberOfCoins;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            _numberOfCoins.Value++;
            _powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
