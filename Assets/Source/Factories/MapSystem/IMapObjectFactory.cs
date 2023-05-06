using Remagures.Model.MapSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IMapObjectFactory
    {
        void Create(IMap map, Transform parent);
    }
}