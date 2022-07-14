using UnityEngine;

public class MagicBottle : PowerUp
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && !collision.isTrigger)
        {
            PowerUpSignal.Invoke();
            Destroy(gameObject);
        }
    }
}
