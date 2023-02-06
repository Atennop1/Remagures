using Remagures.Components;
using Remagures.Interactable;
using Remagures.Inventory;
using Remagures.Model.HealthSystem;
using UnityEngine;
using CharacterInfo = Remagures.SO.CharacterInfo;

namespace Remagures.Model.Character
{
    public class PlayerAttack : Attack
    {
        [Space] 
        [SerializeField] private UniqueSetup _uniqueSetup;
        [SerializeField] private CharacterInfo _characterInfo;

        [Header("Attack Stuff")]
        [SerializeField] private bool _isPlayerAttack;
        [SerializeField] private bool _isPlayerMagic;
        [SerializeField] private bool _isArrow;
        
        public void Init(UniqueSetup uniqueSetup)
            => _uniqueSetup = uniqueSetup;

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Destroyable destroyable))
                destroyable.Smash();

            if (!other.isTrigger) return;
            var health = other.gameObject.GetComponent<Health>();
            if (health == null) return;

            SetupDamage();
            health.TakeDamage((int)_damage);
            
            var enemyFlash = other.gameObject.GetComponent<Flasher>();
            if (enemyFlash == null) return;
            
            if (_characterInfo.FireRunActive)
            {
                var fireEffect = new FireHealthEffect(health, enemyFlash, new FireInfo(1, 3, _damage));
                health.CastHealthEffect(fireEffect);
            }

            Flash(false, other, enemyFlash.DamageColor, _characterInfo.FireRunActive ? enemyFlash.FireColor : enemyFlash.RegularColor);
        }

        private void SetupDamage()
        {
            if (_isPlayerAttack)
            {
                _damage = Mathf.RoundToInt(((IWeaponItem)_uniqueSetup.WeaponSlot.ThisCell.Item).Damage * _characterInfo.SwordDamageCoefficient);
                return;
            }

            if (_isPlayerMagic)
            {
                _damage = Mathf.RoundToInt(((IWeaponItem)_uniqueSetup.MagicSlot.ThisCell.Item).Damage * _characterInfo.MagicDamageCoefficient);
                return;
            }

            if (!_isArrow) return;
            _damage = Mathf.RoundToInt(((IWeaponItem)_uniqueSetup.MagicSlot.ThisCell.Item).Damage * _characterInfo.BowDamageCoefficient);
        }
    }
}
