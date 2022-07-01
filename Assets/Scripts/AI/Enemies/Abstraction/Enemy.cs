using System.Collections;
using UnityEngine;

public enum EnemyState
{
    None,
    Stagger,
    Peace
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAnimations))]
public abstract class Enemy : MonoBehaviour
{
    public EnemyState CurrentState { get; private set; }
    
    [field: SerializeField, Header("Enemy Stuff")] public Signal RoomSignal { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }

    public EnemyAnimations EnemyAnimations { get; private set; }
    public Rigidbody2D MyRigidbody { get; private set; }

    public void Awake()
    {
        EnemyAnimations = GetComponent<EnemyAnimations>();
        CurrentState = EnemyState.None;
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnEnable()
    {
        CurrentState = EnemyState.None;
    }

    public void PeaceInWorld()
    {
        CurrentState = EnemyState.Peace;
    }

    public void ChangeState(EnemyState newState)
    {
        if (CurrentState != newState)
            CurrentState = newState;
    }
}
