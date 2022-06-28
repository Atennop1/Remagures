using UnityEngine;

public class AreaEnemy : EnemyWithTarget
{
    [field: SerializeField, Header("Area Stuff")] public Collider2D Boundary { get; private set; }
}
