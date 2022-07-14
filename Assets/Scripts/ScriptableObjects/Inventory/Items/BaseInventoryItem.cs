using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/BaseItem")]
public class BaseInventoryItem : ScriptableObject, IBaseItemComponent
{
    [field: SerializeField, Header("Item Info")] public string ItemName { get; private set; }
    [field: SerializeField] public string ItemDescription { get; private set; }
    [field: SerializeField] public Sprite ItemSprite { get; private set; }
    [field: SerializeField] public bool Stackable { get; private set; }
}
