using UnityEngine;

[System.Serializable]
public class BaseItemData
{
    [Header("Item Info")]
    [SerializeField] public int NumberHeld;
    [field: SerializeField] public Sprite ItemSprite { get; private set; }

    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public string ItemDescription { get; private set; }
    [field: SerializeField] public bool Stackable { get; private set; }
}
