using UnityEngine;

namespace Remagures.Factories
{
    public interface ILootFactory
    {
        void CreateRandom(Vector3 position);
    }
}