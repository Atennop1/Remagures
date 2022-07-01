using UnityEngine;

public class TurretEnemy : EnemyWithTarget
{
    [field: SerializeField, Header("Turret Stuff")] public RockProjectile RockProjectile { get; private set; }
    [SerializeField] private float _fireDelay;
    
    [HideInInspector] public bool CanFire { get; private set; } = true;
    private float _fireDelaySeconds;

    public void Update()
    {
        _fireDelaySeconds -= Time.deltaTime;
        if (_fireDelaySeconds <= 0)
        {
            CanFire = true;
            _fireDelaySeconds = _fireDelay;
        }
    }
    
    public void InstantiateProjectile(Vector3 tempVector)
    {
        RockProjectile temp = Instantiate(RockProjectile, transform.position, Quaternion.identity);
        CanFire = false;
        temp.Launch(tempVector);
    }
}
