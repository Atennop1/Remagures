using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatTimer : MonoBehaviour
{
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private MeatView _view;

    public float Timer { get; private set; }

    private void OnEnable()
    {
        Timer = 300;
        Timer -= _timeCounter.CheckDate("MeatTime");
        _view.UpdateMeat();
    }

    private void FixedUpdate()
    {
        while (Timer < 0 && _view.RawCount.Value > 0)
        {
            if (Timer <= 0 && Timer > -300)
            {
                Timer = 300;
                _timeCounter.SaveDate("MeatTime");
            }
            else if (Timer < -300)
                Timer += 300;
                
            _view.RawCount.Value--;
            _view.CookedCount.Value++;
            _view.UpdateMeat();
        }

        Timer = 300;

        if (_view.RawCount.Value > 0)
        {
            Timer -= _timeCounter.CheckDate("MeatTime");
            Timer -= Time.deltaTime;
        }
        else
            _timeCounter.SaveDate("MeatTime");

        _view.UpdateTimer();
    }
}
