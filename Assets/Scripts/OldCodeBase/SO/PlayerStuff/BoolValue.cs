using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(fileName = "New BoolValue", menuName = "Player Stuff/BoolValue")]
    [System.Serializable]
    public class BoolValue : ScriptableObject
    {
        public bool Value;
    }
}
