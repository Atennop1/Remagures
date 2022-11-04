using Remagures.SO.Inventory.Items;
using UnityEngine;

namespace Remagures.SO.PlayerStuff
{
    [CreateAssetMenu(fileName = "New ItemValue", menuName = "Player Stuff/ItemValue")]
    [System.Serializable]
    public class ItemValue : ScriptableObject
    {
        public BaseInventoryItem Value;
    }
}
