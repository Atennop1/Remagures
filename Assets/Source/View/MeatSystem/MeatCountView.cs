using Remagures.Model.Notifications;
using UnityEngine;

namespace Remagures.View.MeatSystem
{
    public class MeatCountView : MonoBehaviour
    {
        [SerializeField] private MeatSlotView _rawMeatSlotView;
        [SerializeField] private MeatSlotView _cookedMeatSlotView;

        public void DisplayCookedMeatCount(int count)
            => _cookedMeatSlotView.Display(count);

        public void DisplayRawMeatCount(int count)
            => _rawMeatSlotView.Display(count);
        
        public void Close()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
