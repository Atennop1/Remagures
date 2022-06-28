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
                _movingCoroutine = StartCoroutine(MoveCoroutine(transform.position + (Vector3)moveVector, Speed));
                EnemyAnimations.SetAnimFloat(temp, EnemyAnimations.Anim);
            }
        }
    }

    public void StopMoveCoroutine()
    {
        if (_movingCoroutine != null)
            StopCoroutine(_movingCoroutine);
        CanFindPath = true;
    }

    private IEnumerator MoveCoroutine(Vector3 end, float moveTime)
    {
        float timer = 0f;
        Vector3 startingPos = transform.position;
        CanFindPath = false;
        
        while (timer < moveTime)
        {
            yield return new WaitForFixedUpdate();
            if (CurrentState != EnemyState.Stagger)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(startingPos, end, timer / moveTime);
            }
            else break;
        }
        
        CanFindPath = true;
        if (CurrentState != EnemyState.Stagger)
            transform.position = end;
    }
}