using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class GameObjectFactory : SerializedMonoBehaviour, IGameObjectFactory 
    {
        [SerializeField] private GameObject _prefab;
        
        public GameObject Create()
            => Instantiate(_prefab, transform.position, Quaternion.identity);

        public GameObject Create(Vector3 position)
            => Instantiate(_prefab, position, Quaternion.identity);
    }
}