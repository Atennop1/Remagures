using System.Collections;
using UnityEngine;

public class PlayerHealth : GenericHealth
{
    [Space]
    [SerializeField] private Signal _healthSignal;
    [SerializeField] private FloatValue _heartContainers;

    [Space]
    [SerializeField] private GameOverScript _gameOver;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private Signal _deathSignal;

    [Space]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _music;
    private bool _isStuned;
    
    public void Damage(float amountToDamage, PlayerController player)
    {
        if (player.CurrentState != PlayerState.Dead && !_isStuned)
        {
            int currentDamage = (int)amountToDamage - (int)(player.TotalArmor * player.ClassStat.ArmorCoefficient * player.ClassStat.ShieldRuneCoefficient + 0.5f);
            if (currentDamage <= 0)
                currentDamage = 1;
                
            player.CurrentHealth.Value -= currentDamage;
            _healthSignal.Invoke();
            StartCoroutine(Stun());
            
            if (player.CurrentHealth.Value <= 0)
            {
                player.ChangeState(PlayerState.Dead);
                Instantiate(_deathEffect, transform.position, Quaternion.identity);

                _gameOver.SetGameOver();
                player.CurrentHealth.Value = _heartContainers.Value * 4;
                _gameOver.Init();
                _deathSignal.Invoke();

                _canvas.SetActive(false);
                _music.SetActive(false);
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
