using Remagures.Model.Character;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Components
{
    public class Heart : PowerUp
    {
        [SerializeField] private FloatValue _playerHealth;
        [SerializeField] private FloatValue _heartContainers;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player _) || collision.isTrigger) 
                return;
        
            _playerHealth.Value += 4;
            if (_playerHealth.Value > _heartContainers.Value * 4)
                _playerHealth.Value = _heartContainers.Value * 4;

            PowerUpSignal.Invoke();
            Destroy(gameObject);
        }
    }
}
