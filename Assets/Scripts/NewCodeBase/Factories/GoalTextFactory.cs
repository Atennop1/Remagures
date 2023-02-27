using TMPro;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class GoalTextFactory : MonoBehaviour, IGoalTextFactory
    {
        [SerializeField] private TextMeshProUGUI _textPrefab;
        
        public TextMeshProUGUI Create(Transform parent) 
            => Instantiate(_textPrefab, parent);
    }
}