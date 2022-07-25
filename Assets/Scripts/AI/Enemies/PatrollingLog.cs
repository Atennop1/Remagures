using UnityEngine;

public class PatrollingLog : EnemyWithTarget
{
    [field: SerializeField, Header("Patrolling Stuff")] public Transform[] Path { get; private set; }

    [HideInInspector] public Transform CurrentGoal { get; private set; }
    [HideInInspector] public int CurrentPoint { get; private set; } = 0;

    public override void Start()
    {
        base.Start();
        EnemyAnimations.Animator.SetBool("wakeUp", true);
    }

    public void ChangeGoal()
    {
        if (CurrentPoint == Path.Length - 1)
            CurrentPoint = 0;
        else
            CurrentPoint++;

        CurrentGoal = Path[CurrentPoint];
    }
    
    public override void Move(Transform targetVector)
    {
        base.Move(targetVector);

        if (targetVector == Path[CurrentPoint] && Pathfinding.Path.Count == 1)
            ChangeGoal();
    }
}
