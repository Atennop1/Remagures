using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ItemsData/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; }
    }
}