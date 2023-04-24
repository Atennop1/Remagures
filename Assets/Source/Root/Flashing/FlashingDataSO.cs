using Remagures.Model.Flashing;
using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "FlashingData", menuName = "Flashing/FlashingData")]
    public sealed class FlashingDataSO : ScriptableObject, IFlashingData
    {
        [field: SerializeField] public int FlashDurationInMilliseconds { get; private set; }
        [field: SerializeField] public int NumberOfFlashes { get; private set; }
    }
}