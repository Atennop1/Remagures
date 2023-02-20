using UnityEngine;

namespace Remagures.Factories
{
    public class GameObjectFactory : MonoBehaviour, IGameObjectFactory //TODO make for this loot factory
    {
        [SerializeField] private GameObject _prefab;
        
        public GameObject Create(Vector3 position)
            => Instantiate(_prefab, position, Quaternion.identity);
    }
}