using Remagures.SO.PlayerStuff;
using UnityEngine;

namespace Remagures.Components.PowerUps
{
    public class Coin : PowerUp
    {
        [SerializeField] private FloatValue _numberOfCoins;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player.Player _) || collision.isTrigger) return;
        
            _numberOfCoins.Value++;
            PowerUpSignal.Invoke();
            Destroy(gameObject);
        }
    }
}
