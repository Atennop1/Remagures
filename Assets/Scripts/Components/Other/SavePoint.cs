using Remagures.SaveSystem;
using Remagures.SO.PlayerStuff;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.Components.Other
{
    public class SavePoint : MonoBehaviour
    {
        [SerializeField] private VectorValue _playerPosition;
        [FormerlySerializedAs("_saveContainer")] [SerializeField] private GameSaver _gameSaver;
    
        public void OnTriggerEnter2D(Collider2D col)
        {
            _playerPosition.SetValue(transform.position + Vector3.up / 2);
            _gameSaver.SaveGame();
        }
    }
}