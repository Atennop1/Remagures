using UnityEngine;

namespace Remagures.Components.PowerUps
{
    public class MagicBottle : PowerUp
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player.Player _) || collision.isTrigger) return;
        
            PowerUpSignal.Invoke();
            Destroy(gameObject);
        }
    }
}
