using System;
using UnityEngine;

namespace Remagures.Time
{
    public class TimeCounter : MonoBehaviour
    {
        private DateTime _currentDate;
        private DateTime _oldDate;

        public float CheckDate(string key)
        {
            _currentDate = DateTime.Now;
            var tempString = PlayerPrefs.GetString(key, "1");
            var tempLong = Convert.ToInt64(tempString);
        
            _oldDate = DateTime.FromBinary(tempLong);
            var difference = _currentDate.Subtract(_oldDate);
            return (float)difference.TotalSeconds;
        }
    
        public void SaveDate(string key)
        {
            PlayerPrefs.SetString(key, DateTime.Now.ToBinary().ToString());
        }
    }
}
