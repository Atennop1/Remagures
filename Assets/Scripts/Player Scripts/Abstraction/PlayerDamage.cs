using UnityEngine;

public class PlayerDamage : GenericDamage
{
    [Header("Attack Stuff")]
    [SerializeField] private bool _isPlayerAttack;
    [SerializeField] private bool _isPlayerMagic;
    [SerializeField] private bool _isArrow;

    [field: SerializeField, Space] public Player Player { get; private set; }

    public void Init(Player player)
    {
        Player = player;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Destroyable>(out Destroyable destroyable))
            destroyable.Smash();
            
        if (other.isTrigger)
        {
            SetDamage();
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

    private void SetDamage()
    {
        if (_isPlayerAttack)
        {
            _damage = Mathf.RoundToInt((Player.UniqueSetup.WeaponSlot.ThisCell.Item as IWeaponItem).Damage * Player.ClassStat.SwordDamageCoefficient);
            return;
        }

        if (_isPlayerMagic)
        {
            _damage = Mathf.RoundToInt((Player.UniqueSetup.MagicSlot.ThisCell.Item as IWeaponItem).Damage * Player.ClassStat.MagicDamageCoefficient);
            return;
        }

        if (_isArrow)
        {
            _damage = Mathf.RoundToInt((Player.UniqueSetup.MagicSlot.ThisCell.Item as IWeaponItem).Damage * Player.ClassStat.BowDamageCoefficient);
            return;
        }
    }
}
