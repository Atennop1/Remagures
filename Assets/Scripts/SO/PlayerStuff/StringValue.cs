using UnityEngine;

namespace Remagures.SO.PlayerStuff
{
    [CreateAssetMenu(fileName = "New StringValue", menuName = "Player Stuff/StringValue")]
    [System.Serializable]
    public class StringValue : ScriptableObject
    {
        public string Value;
    }
}