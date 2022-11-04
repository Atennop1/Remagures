using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Components.Base;
using Remagures.Player.Components;
using Remagures.SO.PlayerStuff;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Player
{
    public class Player : SerializedMonoBehaviour
    {
        [SerializeField] private VectorValue _playerPosition;
        [SerializeField] private StringValue _currentScene;
        
        [Space]
        [SerializeField] private Health _health;
        [field: SerializeField] public PublicData Data { get; private set; }
        
        [Space]
        [SerializeField] private List<IPlayerComponent> _playerComponents = new();

        public PlayerState CurrentState { get; private set; }

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