using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Components.Base;
using Remagures.Player.Components;
using Remagures.SO.PlayerStuff;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Player
{
    public class Player : SerializedMonoBehaviour
    {
        [SerializeField] private StringValue _currentScene;
        [SerializeField] private VectorValue _playerPosition;

        [Space]
        [SerializeField] private Health _health;
        [OdinSerialize] private PublicPlayerData _playerData;
        
        [Space]
        [OdinSerialize] private List<IPlayerComponent> _playerComponents = new();

        public PlayerState CurrentState { get; private set; }
        public PublicPlayerData PlayerData => _playerData;

        public T GetPlayerComponent<T>() where T: IPlayerComponent
        {
            foreach (var component in _playerComponents.Where(component => component.GetType() == typeof(T)))
                return (T)component;

            throw new ArgumentException($"Player hasn't {typeof(T)} type");
        }

        private void Awake()
        {
            _currentScene.Value = SceneManager.GetActiveScene().name;
            transform.position = _playerPosition.Value - new Vector2(0, 0.6f);
            CurrentState = PlayerState.Walk;
        }

        public void ChangeState(PlayerState newState)
        {
            if (newState == PlayerState.Dead && _health.Value > 0)
                throw new InvalidOperationException();

            if (newState != CurrentState)
                CurrentState = newState;
        }
    }
}