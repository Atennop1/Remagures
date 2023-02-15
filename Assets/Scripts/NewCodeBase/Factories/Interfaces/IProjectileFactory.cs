using Remagures.Model;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IProjectileFactory
    {
        IProjectile Create(Quaternion rotation);
    }
}