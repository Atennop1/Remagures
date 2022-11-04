using System.Collections.Generic;
using System.Linq;
using Remagures.Components.HealthEffects;
using UnityEngine;

namespace Remagures.Components.Base
{
    public class Health : MonoBehaviour
    {
        [field: SerializeField, Space] public LayerMask InteractionMask { get; private set; }
        public int Value { get; private set; }
        public bool IsDied => Value <= 0;
    
        private List<IHealthEffect> _effects;
        private bool _isMaxHealthSet;

        public virtual void TakeDamage(int damage)
        {
            Value -= damage;
        }

        protected void SetStartHealth(int value)
        {
            if (_isMaxHealthSet) return;

            Value = value;
            _isMaxHealthSet = true;
        }

        public void CastHealthEffect(IHealthEffect castingEffect)
        {
            if (NeedUnCastHealthEffect(castingEffect))
                UnCastHealthEffect(castingEffect);

            _effects.Add(castingEffect);
            castingEffect.Activate();
        }

        public void UnCastHealthEffect(IHealthEffect castingEffect)
        {
            if (!NeedUnCastHealthEffect(castingEffect)) return;
            
            castingEffect.Stop();
            _effects.Remove(castingEffect);
        }
        
        private bool NeedUnCastHealthEffect(IHealthEffect castingEffect) =>_effects.Where(effect => effect.GetType() == castingEffect.GetType()).ToList().Count > 0;

        private void Awake()
        {
            _effects = new List<IHealthEffect>();
        }
    }
}