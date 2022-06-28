using UnityEngine;

public class MagicBottle : PowerUp
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            _powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
