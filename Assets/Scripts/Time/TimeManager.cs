using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    private DateTime _currentDate;
    private DateTime _oldDate;

    public float CheckDate(string key)
    {
        _currentDate = System.DateTime.Now;
        string tempString = PlayerPrefs.GetString(key, "1");
        long tempLong = Convert.ToInt64(tempString);
        _oldDate = DateTime.FromBinary(tempLong);
        TimeSpan difference = _currentDate.Subtract(_oldDate);
        return (float)difference.TotalSeconds;
    }
    
    public void SaveDate(string key)
    {
        PlayerPrefs.SetString(key, System.DateTime.Now.ToBinary().ToString());
    }
}
