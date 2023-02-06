using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(fileName = "New VectorValue", menuName = "Player Stuff/VectorValue")]
    [System.Serializable]
    public class VectorValue : ScriptableObject
    {
        [SerializeField] private float _xValue;
        [SerializeField] private float _yValue;

        public Vector2 Value 
            => new(_xValue, _yValue);

        public void SetValue(Vector2 value)
        {
            _xValue = value.x;
            _yValue = value.y;
        }
    }
}
