using UnityEngine;
using UnityEngine.UI;

public class MeatManager : MonoBehaviour
{
    
    [Header("Objects")]
    [SerializeField] private MeatSlot _rawMeatObject;
    [SerializeField] private MeatSlot _cookedMeatObject;
    [SerializeField] private Slider _readySlider;
    [SerializeField] private Text _timeText;
    [SerializeField] private TimeManager _timeManager;

    [Header("Variables")]
    [SerializeField] private FloatValue _rawCount;
    [SerializeField] private FloatValue _cookedCount;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private BaseInventoryItem _cookedMeatItem;
    [SerializeField] private BaseInventoryItem _rawMeatItem;

    private float _timer;

    private void Start() 
    {
        _timeText.text = "0:00";
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _timer = 300;
        _timer -= _timeManager.CheckDate("MeatTime");
        UpdateMeat();
    }

    private void Update()
    {
        while (_timer < 0 && _rawCount.Value > 0)
        {
            if(_timer <= 0 && _timer > -300)
            {
                _timer = 300;
                _timeManager.SaveDate("MeatTime");
            }
            else if (_timer < -300)
                _timer += 300;
            _rawCount.Value--;
            _cookedCount.Value++;
            UpdateMeat();
        }
        if (_rawCount.Value > 0)
        {
            _timer = 300;
            _timer -= _timeManager.CheckDate("MeatTime");
            _timeText.text = (int)(_timer / 60) + ":" + ((int)(_timer % 60)).ToString().PadLeft(2, '0');
            _readySlider.value = (300 - _timer) / 300;
            _timer -= Time.deltaTime;
        }
        else
        {
            _timeText.text = "0:00";
            _readySlider.value = 0;
            _timer = 300;
            _timeManager.SaveDate("MeatTime");
        }
    }

    private void UpdateMeat()
    {
        _cookedMeatObject.Setup((int)_cookedCount.Value);
        _rawMeatObject.Setup((int)_rawCount.Value);
    }

    public void Loot()
    {
        _cookedMeatItem.ItemData.NumberHeld += (int)_cookedCount.Value;
        _cookedCount.Value = 0;
        _inventory.Add(_cookedMeatItem, false);
        UpdateMeat();
    }

    public void Put()
    {
        _rawCount.Value += _rawMeatItem.ItemData.NumberHeld;
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
