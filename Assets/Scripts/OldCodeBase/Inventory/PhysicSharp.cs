using Remagures.Model.Character;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Inventory
{
    public class PhysicSharp : MonoBehaviour
    {
        [SerializeField] private FloatValue _sharps;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Player _) || other.isTrigger) 
                return;
        
            _sharps.Value++;
            Destroy(gameObject);
        }
    }
}