using Remagures.Model.Projectiles;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IProjectileFactory
    {
        IProjectile Create(Quaternion rotation);
    }
}