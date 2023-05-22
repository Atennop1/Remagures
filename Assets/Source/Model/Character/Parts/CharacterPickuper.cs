using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class CharacterPickuper : SerializedMonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.gameObject.TryGetComponent(out IPickupable pickupable)) 
                return;
            
            pickupable.Pickup();
            Destroy(collider2d.gameObject);
        }
    }
}