using System.Collections;
using UnityEngine;

namespace Remagures.Components.Base
{
    [RequireComponent(typeof(Collider2D))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] protected float _damage;

        private Flasher _flash;
        private bool _isStunned;

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Health health) ||
                health.InteractionMask != (health.InteractionMask | (1 << gameObject.layer)) || !other.isTrigger) return;

            if (health == null) return;
        
            health.TakeDamage((int)_damage);
            _flash = other.gameObject.transform.parent.GetComponent<Flasher>();

            if (_flash != null && !_isStunned)
                Flash(true, other, _flash.DamageColor, _flash.RegularColor);
        }
    
        protected void Flash(bool isPlayer, Collider2D other, Color flashColor, Color afterFlashColor)
        {
            StartCoroutine(Stun());
            var newObject = isPlayer ? other.gameObject.transform.parent.gameObject : other.gameObject;

            FlashObject(newObject, flashColor, afterFlashColor);
            foreach (Transform child in newObject.transform)
                FlashObject(child.gameObject, flashColor, afterFlashColor);
        }

        private void FlashObject(GameObject gameObj, Color flashColor, Color afterFlashColor)
        {
            var flash = gameObj.GetComponent<Flasher>();
            if (flash != null)
                flash.StartFlashCoroutine(flashColor, afterFlashColor);
        }

        private IEnumerator Stun()
        {
            _isStunned = true;
            yield return null;
            _isStunned = false;
        }
    }
}
