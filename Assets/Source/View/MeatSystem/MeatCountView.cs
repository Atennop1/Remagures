using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.MeatSystem
{
    public class MeatCountView : SerializedMonoBehaviour, IMeatCountView
    {
        [SerializeField] private IMeatSlotView _rawMeatSlotView;
        [SerializeField] private IMeatSlotView _cookedMeatSlotView;

        public void DisplayCookedMeatCount(int count)
            => _cookedMeatSlotView.Display(count);

        public void DisplayRawMeatCount(int count)
            => _rawMeatSlotView.Display(count);
    }
}
