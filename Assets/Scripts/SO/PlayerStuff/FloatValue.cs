using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(fileName = "New FloatValue", menuName = "Player Stuff/FloatValue")]
    [System.Serializable]
    public class FloatValue : ScriptableObject
    {
        public float Value;
    }
}
