using UnityEngine;

public class RockProjectile : Projectile
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
