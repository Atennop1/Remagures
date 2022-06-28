using Unity.Notifications.Android;

public class WeeklyNotificationComponent : NotificationComponent
{
    public static WeeklyNotificationComponent Instance { get; private set; }

    protected override void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        base.Awake();
    }

    private void Start() 
    {
        AndroidNotificationCenter.CancelNotification(0);
        _canNotify = true;
    }
    
    protected override void OnApplicationPause(bool focusStatus)
    {
        if (focusStatus)
        {
            _isFocus = false;
            _delay = 3600 * 24 * 7;
            SendNotification(0);
        }
        else
            Start();
    }
}