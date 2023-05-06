using Remagures.Model.MapSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IMapObjectFactory
    {
        GameObject Create(IMap map, Transform parent);
    }
}