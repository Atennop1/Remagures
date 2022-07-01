using UnityEngine;
using UnityEngine.UI;

public class MeatView : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private MeatSlot _rawMeatObject;
    [SerializeField] private MeatSlot _cookedMeatObject;
    [SerializeField] private Slider _readySlider;
    [SerializeField] private Text _timeText;
    [SerializeField] private MeatTimer _timer;

    [field: SerializeField, Header("Variables")] public FloatValue RawCount { get; private set; }
    [field: SerializeField] public FloatValue CookedCount { get; private set; }
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private BaseInventoryItem _cookedMeatItem;
    [SerializeField] private BaseInventoryItem _rawMeatItem;

    private void Start() 
    {
        _timeText.text = "0:00";
        gameObject.SetActive(false);
    }

    public void UpdateMeat()
    {
        _cookedMeatObject.Setup((int)CookedCount.Value);
        _rawMeatObject.Setup((int)RawCount.Value);
    }

    public void UpdateTimer()
    {
        if (RawCount.Value > 0)
        {
            _timeText.text = (int)(_timer.Timer / 60) + ":" + ((int)(_timer.Timer % 60)).ToString().PadLeft(2, '0');
            _readySlider.value = (300 - _timer.Timer) / 300;
        }
        else
        {
            _timeText.text = "0:00";
            _readySlider.value = 0;
        }
    }

    public void Loot()
    {
        _cookedMeatItem.ItemData.NumberHeld += (int)CookedCount.Value;
        CookedCount.Value = 0;
        _inventory.Add(_cookedMeatItem, false);
        UpdateMeat();
    }

    public void Put()
    {
        RawCount.Value += _rawMeatItem.ItemData.NumberHeld;
        _rawMeatItem.ItemData.NumberHeld = 0;
        _inventory.Remove(_rawMeatItem);
        UpdateMeat();
        Unity.Notifications.Android.AndroidNotificationCenter.CancelNotification(1);
        MeatNotificationComponent.Instance.Init();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
