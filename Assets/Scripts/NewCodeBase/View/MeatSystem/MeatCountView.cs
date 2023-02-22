using Remagures.Model.InventorySystem;
using Remagures.Notifications;
using UnityEngine;

namespace Remagures.Model.MeatSystem
{
    public class MeatCountView : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private MeatSlotView _rawMeatSlotView;
        [SerializeField] private MeatSlotView _cookedMeatSlotView;

        public void DisplayCookedMeatCount(int count)
            => _cookedMeatSlotView.Display(count);

        public void DisplayRawMeatCount(int count)
            => _rawMeatSlotView.Display(count);

        public void Put() //TODO remove
        {
            Unity.Notifications.Android.AndroidNotificationCenter.CancelNotification(1);
            MeatNotificationComponent.Instance.Send();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
