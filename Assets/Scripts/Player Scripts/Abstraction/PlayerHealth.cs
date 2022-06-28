using System.Collections;
using UnityEngine;

public class PlayerHealth : GenericHealth
{
    [Space]
    [SerializeField] private Signal _healthSignal;
    [SerializeField] private FloatValue _currentHealth;
    [SerializeField] private FloatValue _heartContainers;

    [Space]
    [SerializeField] private GameOverScript _gameOver;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private Signal _deathSignal;
    private bool _isStuned;
    
    public void Damage(float amountToDamage, PlayerController player)
    {
        if (player.CurrentState != PlayerState.Dead && !_isStuned)
        {
            int currentDamage = (int)amountToDamage - (int)(player.TotalArmor * player.ClassStat.ArmorCoefficient * player.ClassStat.ShieldRuneCoefficient + 0.5f);
            if (currentDamage <= 0)
                currentDamage = 1;
                
            _currentHealth.Value -= currentDamage;
            _healthSignal.Raise();
            StartCoroutine(Stun());
            
            if (_currentHealth.Value <= 0)
            {
                player.CurrentState = PlayerState.Dead;
                Instantiate(_deathEffect, transform.position, Quaternion.identity);

                _gameOver.SetGameOver();
                _currentHealth.Value = _heartContainers.Value * 4;
                _gameOver.Init();
                _deathSignal.Raise();

                GameObject.Find("Canvas").SetActive(false);
                GameObject.Find("Music").SetActive(false);
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator Stun()
    {
        _isStuned = true;
        yield return null;
        _isStuned = false;
    }
}
