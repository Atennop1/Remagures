using Remagures.Model.UpgradeSystem;
using Remagures.Model.Wallet;
using UnityEngine;

namespace Remagures.Root
{
    [CreateAssetMenu(fileName = "New upgrade buying data", menuName = "UpgradeSystem/BuyingData", order = 0)]
    public sealed class UpgradeBuyingDataSO : ScriptableObject, IUpgradeBuyingData
    {
        [field: SerializeField] public Currency Currency { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
    }
}