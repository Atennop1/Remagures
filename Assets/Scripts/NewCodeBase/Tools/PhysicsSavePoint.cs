using Remagures.Model.Character;
using SaveSystem;
using SaveSystem.Paths;
using UnityEngine;

namespace Remagures.Tools
{
    public sealed class PhysicsSavePoint : MonoBehaviour
    {
        private readonly ISaveStorage<CharacterPositionData> _storage = new BinaryStorage<CharacterPositionData>(new Path("CharacterPositionData"));

        public void OnTriggerEnter2D(Collider2D col)
        {
            var savedPositionData = _storage.Load();
            _storage.Save(new CharacterPositionData(transform.position + Vector3.up / 2, savedPositionData.CharacterLookDirection));
        }
    }
}
