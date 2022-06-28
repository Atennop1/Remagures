using UnityEngine;

[CreateAssetMenu(fileName = "New VectorValue", menuName = "Player Stuff/VectorValue")]
[System.Serializable]
public class VectorValue : ScriptableObject
{
    public Vector2 Value;
}
