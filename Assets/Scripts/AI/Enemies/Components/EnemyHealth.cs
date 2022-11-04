using Remagures.AI.Enemies.Abstraction;
using Remagures.Components.Base;
using Remagures.SO.Other;
using Remagures.SO.PlayerStuff;
using UnityEngine;

namespace Remagures.AI.Enemies.Components
{
    public class EnemyHealth : Health
    {
        [Header("Health Stuff")]
        [SerializeField] private FloatValue _maxHealth;
        [SerializeField] private LootTable _lootTable;
        [SerializeField] private GameObject _deathEffect;
        
        private bool _isStunned;

        public override void TakeDamage(int damage)
        {
            if (IsDied) return;
            if (_isStunned) return;
        
            base.TakeDamage(damage);
            if (!IsDied) return;
        
            DeathEffect();
            MakeLoot();

            gameObject.SetActive(false);
        }
        
        private void Start()
        {
            SetStartHealth(_maxHealth.Value > 0 ? (int)_maxHealth.Value : 0);
        }

        private void DeathEffect()
        {
            var effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        private void MakeLoot()
        {
            if (_lootTable == null) return;
        
            var current = _lootTable.Loot();
            if (current != null)
                Instantiate(current, transform.position + new Vector3(0, 0.0000001f, 0), Quaternion.identity);
        }
    }
}
