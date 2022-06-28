using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UsableItemData
{
    [field: SerializeField] public UnityEvent ThisEvent { get; private set; }
}
