using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Pathfinding2D))]
public class EnemyWithTarget : Enemy
{
    [field: SerializeField, Header("Target Stuff")] public float ChaseRadius { get; private set; }
    [field: SerializeField] public float AttackRadius { get; private set; }
    [field: SerializeField] public Transform Target { get; private set; }

    public Pathfinding2D Pathfinding { get; private set; }
    public bool CanFindPath { get; private set; }

    private Coroutine _movingCoroutine;

    public virtual void Start()
    {
        Pathfinding = GetComponent<Pathfinding2D>();
        CanFindPath = true;
    }

    public virtual void Move(Vector3 moveTo)
    {
        if (CanFindPath && CurrentState != EnemyState.Stagger) 
        {
            Pathfinding.FindPath(transform.position, moveTo);
            if (Pathfinding.Path != null && Pathfinding.Path.Count > 0)
            {
                Vector2 temp = Pathfinding.Path[0].WorldPosition + new Vector3(0, 0.5f, 0) - transform.position;
                Vector2 moveVector = temp.normalized;

                StopMoveCoroutine();
                _movingCoroutine = StartCoroutine(MoveCoroutine(moveVector, Speed));
                EnemyAnimations.SetAnimFloat(temp, EnemyAnimations.Animator);
            }
        }
    }

    public void StopMoveCoroutine()
    {
        if (_movingCoroutine != null)
            StopCoroutine(_movingCoroutine);
        CanFindPath = true;
    }

    private IEnumerator MoveCoroutine(Vector3 direction, float speed)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction;
        CanFindPath = false;

        while ((transform.position.x < targetPosition.x - 0.1f || transform.position.x > targetPosition.x + 0.1f) ||
               (transform.position.y < targetPosition.y - 0.1f || transform.position.y > targetPosition.y + 0.1f))
        {
            Vector3 temp = transform.position + speed * Time.deltaTime * direction;
            MyRigidbody.MovePosition(temp);
            yield return new WaitForFixedUpdate();
        }
        
        CanFindPath = true;
    }
}