using Remagures.Model.Magic;
using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "New MagicData", menuName = "Magic/Data", order = 0)]
    public sealed class MagicDataSO : ScriptableObject, IMagicData
    {
        [field: SerializeField] public int ManaCost { get; private set; }
        [field: SerializeField] public int CooldownInMilliseconds { get; private set; }
        [field: SerializeField] public int ApplyingTimeInMilliseconds { get; private set; }
    }
}