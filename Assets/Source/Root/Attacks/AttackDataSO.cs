using Remagures.Model.Attacks;
using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "Attack Data", menuName = "Attacks/Data", order = 0)]
    public sealed class AttackDataSO : ScriptableObject, IAttackData
    {
        [field: SerializeField] public int UsingCooldownInMilliseconds { get; private set; }
        [field: SerializeField] public int AttackingTimeInMilliseconds { get; private set; }
    }
}