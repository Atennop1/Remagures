using System.Collections;
using UnityEngine;

namespace Remagures.Rooms
{
    public class RoomTransition : MonoBehaviour
    {
        [SerializeField] private Vector3 _playerChange;
        private Collider2D _lastCollider;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player.Player _) || _lastCollider != null) return;
        
            _lastCollider = collision;
            StartCoroutine(LastColReset());
            collision.gameObject.transform.position += new Vector3(_playerChange.x, _playerChange.y, 0);
        }

        private IEnumerator LastColReset()
        {
            yield return null;
            _lastCollider = null;
        }
    }
}
