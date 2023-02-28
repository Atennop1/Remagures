using TMPro;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IGoalTextFactory
    {
        TextMeshProUGUI Create(Transform parent);
    }
}