using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(fileName = "New ItemValue", menuName = "Player Stuff/ItemValue")]
    [System.Serializable]
    public class ItemValue : ScriptableObject
    {
        public BaseInventoryItem Value;
    }
}
