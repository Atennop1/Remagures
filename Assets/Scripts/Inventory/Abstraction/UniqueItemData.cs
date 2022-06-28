using UnityEngine;

[System.Serializable]
public class UniqueItemData
{
    [field: SerializeField, Header("Unique Stuff")] public UniqueClass UniqueClass { get; private set; }
}
