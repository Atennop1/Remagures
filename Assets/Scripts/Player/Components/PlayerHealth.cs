using System;
using System.Collections;
using Remagures.Components;
using Remagures.SO;
using UnityEngine;
using CharacterInfo = Remagures.SO.CharacterInfo;

namespace Remagures.Player
{
    public class PlayerHealth : Health, IPlayerComponent
    {
        [SerializeField] private FloatValue _currentHealth;
        [SerializeField] private CharacterInfo _characterInfo;
        [SerializeField] private Player _player;
    
        [Space]
        [SerializeField] private Signal _healthSignal;
        [SerializeField] private FloatValue _heartContainers;

        [Space]
        [SerializeField] private GameOverHandler _gameOver;
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private Signal _deathSignal;

        [Space]
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _music;
        private int _totalArmor;
        private bool _isStunned;
    
        public override void TakeDamage(int amountToDamage)
        {
            if (_player.CurrentState == PlayerState.Dead || _isStunned || IsDied) return;
        
            var currentDamage = amountToDamage - (int)(_totalArmor * _characterInfo.ArmorCoefficient * _characterInfo.ShieldRuneCoefficient + 0.5f);
            if (currentDamage <= 0)
                currentDamage = 1;
                
            _currentHealth.Value -= currentDamage;
            base.TakeDamage(currentDamage);
            
            _healthSignal.Invoke();
            StartCoroutine(Stun());

            if (IsDied) Death();
        }

        public void SetTotalArmor(int value)
        {
            if (value < 0)
                throw new ArgumentException("Can't set a negative total number");
            
            if (value <= _totalArmor) 
                return;

            _totalArmor = value;
        }

        private void Death()
        {
            _player.ChangeState(PlayerState.Dead);
            Instantiate(_deathEffect, transform.position, Quaternion.identity);

            _gameOver.SetGameOver();
            _currentHealth.Value = _heartContainers.Value * 4;
        
            _gameOver.Init();
            _deathSignal.Invoke();

            _canvas.SetActive(false);
            _music.SetActive(false);
            gameObject.transform.parent.gameObject.SetActive(false);
        }

        private IEnumerator Stun()
        {
            _isStunned = true;
            yield return null;
            _isStunned = false;
        }

        private void Awake() => SetStartHealth((int)_currentHealth.Value);
    }
}
