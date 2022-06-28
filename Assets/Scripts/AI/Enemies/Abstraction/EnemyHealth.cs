using System.Collections;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
    [Header("Health Stuff")]
    [SerializeField] private FloatValue _maxHealth;
    [SerializeField] private LootTable _lootTable;
    [SerializeField] private GameObject _deathEffect;

    private Enemy _enemy;
    private float _health;
    private bool _isStuned;

    public void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        _health = _maxHealth.Value > 0 ? _maxHealth.Value : 0;
        TakeDamage(0);
    }

    public void TakeDamage(float damage)
    {
        if (!_isStuned)
        {
            _health -= damage;
            if (_health <= 0)
            {
                DeathEffect();
                MakeLoot();

                if (_enemy.RoomSignal != null)
                    _enemy.RoomSignal.Raise();

                gameObject.SetActive(false);
            }
        }
    }

    private void DeathEffect()
    {
        GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

    private void MakeLoot()
    {
        if (_lootTable != null)
        {
            GameObject current = _lootTable.Loot();
            if (current != null)
                Instantiate(current, transform.position + new Vector3(0, 0.0000001f, 0), Quaternion.identity);
        }
    }

    private IEnumerator Stun()
    {
        _isStuned = true;
        yield return new WaitForSeconds(0.2f);
        _isStuned = false;
    }
}
