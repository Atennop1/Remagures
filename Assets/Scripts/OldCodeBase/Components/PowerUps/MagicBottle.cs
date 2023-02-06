using Remagures.Model.Character;
using UnityEngine;

namespace Remagures.Components
{
    public class MagicBottle : PowerUp
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player _) || collision.isTrigger) 
                return;
        
            PowerUpSignal.Invoke();
            Destroy(gameObject);
        }
    }
}
