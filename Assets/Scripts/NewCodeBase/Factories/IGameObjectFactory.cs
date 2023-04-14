using UnityEngine;

namespace Remagures.Factories
{
    public interface IGameObjectFactory
    {
        GameObject Create();
        GameObject Create(Vector3 position);
    }
}