using UnityEngine;

namespace Remagures.Model.Projectiles
{
    public interface IProjectile
    {
        void Launch(Vector2 direction);
    }
}