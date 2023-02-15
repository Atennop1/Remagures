using Remagures.Model;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IProjectilesFactory
    {
        IProjectile Create(Quaternion rotation);
    }
}