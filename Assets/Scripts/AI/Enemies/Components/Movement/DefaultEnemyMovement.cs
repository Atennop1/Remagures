using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.AI.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DefaultEnemyMovement : MonoBehaviour, IEnemyMovement
    {
        [FormerlySerializedAs("Speed")] [field: SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;
        private EnemyAnimations _enemyAnimations;

        private Pathfinding2D _pathfinding;
        private Coroutine _movingCoroutine;
        
        public bool CanMove { get; private set; }

        public void Move(Transform targetTransform)
        {
            if (!CanMove) return;
            
            _pathfinding.FindPath(transform.position, targetTransform.position +
                                                     (targetTransform.gameObject.TryGetComponent<Player.Player>(out _)
                                                         ? new Vector3(0, 0.6f, 0)
                                                         : Vector3.zero));

            if (_pathfinding.Path is not { Count: > 0 }) return;

            var firstMovePoint = _pathfinding.Path[0].WorldPosition + new Vector3(0, 0.5f, 0);

            StopMoving();
            _movingCoroutine = StartCoroutine(MoveCoroutine(firstMovePoint, _speed));
            _enemyAnimations?.SetAnimFloat(firstMovePoint - transform.position, _enemyAnimations.Animator);
        }

        public void StopMoving()
        {
            if (_movingCoroutine != null)
                StopCoroutine(_movingCoroutine);
            CanMove = true;
        }
        
        private IEnumerator MoveCoroutine(Vector3 targetPosition, float speed)
        {
            CanMove = false;
            while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                var temp = transform.position + speed * UnityEngine.Time.deltaTime * (targetPosition - transform.position).normalized;
                _rigidbody.MovePosition(temp);
                yield return new WaitForFixedUpdate();
            }
            
            transform.position = targetPosition;
            CanMove = true;
        }

        private IEnumerator CanFindPathCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                CanMove = true;
            }
        }
        
        private void Start()
        {
            _pathfinding = GetComponent<Pathfinding2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyAnimations = GetComponent<EnemyAnimations>();
            
            StartCoroutine(CanFindPathCoroutine());
            CanMove = true;
        }
    }
}