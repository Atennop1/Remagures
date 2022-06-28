using UnityEngine;

public class PlayerDamage : GenericDamage
{
    [Header("Attack Stuff")]
    [SerializeField] private bool _isPlayerAttack;
    [SerializeField] private bool _isPlayerMagic;
    [SerializeField] private bool _isArrow;

    [field: SerializeField] public PlayerController Player { get; private set; }

    public void Init(PlayerController player)
    {
        Player = player;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            if (_isPlayerAttack)
                _damage = Mathf.RoundToInt((Player.UniqueManager.WeaponSlot.ThisItem as WeaponInventoryItem).WeaponItemData.Damage * Player.ClassStat.SwordDamageCoefficient);

            if (_isPlayerMagic)
                _damage = Mathf.RoundToInt((Player.UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).WeaponItemData.Damage * Player.ClassStat.MagicDamageCoefficient);

            if (_isArrow)
                _damage = Mathf.RoundToInt((Player.UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).WeaponItemData.Damage * Player.ClassStat.BowDamageCoefficient);
            
            EnemyHealth temp = other.gameObject.GetComponent<EnemyHealth>();
            if (temp != null)
            {
                temp.TakeDamage(_damage);
                GenericFlash enemyFlash = other.gameObject.GetComponent<GenericFlash>();

                if (enemyFlash != null)
                {
                    if (Player.ClassStat.FireRunActive)
                        temp.StartFireCoroutine(enemyFlash, _damage, temp.TakeDamage);

                    Flash(false, other, enemyFlash.DamageColor, Player.ClassStat.FireRunActive ? enemyFlash.FireColor : enemyFlash.RegularColor);
                }
            }
        }
    }
}
