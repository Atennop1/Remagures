using UnityEngine;

[CreateAssetMenu(fileName = "New StringValue", menuName = "Player Stuff/StringValue")]
[System.Serializable]
public class StringValue : ScriptableObject
{
    public string Value;
}
