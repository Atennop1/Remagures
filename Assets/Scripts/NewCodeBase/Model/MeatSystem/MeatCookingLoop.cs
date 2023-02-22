using Remagures.Root;
using Remagures.Tools;
using Remagures.View.MeatSystem;
using UnityEngine;

namespace Remagures.Model.MeatSystem
{
    public class MeatCookingLoop : IUpdatable
    {
        private readonly TimeCounter _timeCounter = new(new BinaryStorage());
        private readonly MeatCookingTimerView _meatCookingTimerView;
        private readonly MeatCooker _meatCooker;

        private float _remainingCookingTime;
        private bool _isLoopActive;

        public void ActivateLoop()
        {
            _isLoopActive = true;
            _remainingCookingTime = 300;

            if (_meatCooker.RawMeatCount > 0)
                _remainingCookingTime -= _timeCounter.GetTimeDifference("MeatTime");
        }

        public void Update()
        {
            if (_meatCooker.RawMeatCount <= 0 || _isLoopActive == false)
            {
                _isLoopActive = false;
                return;
            }
            
            while (_remainingCookingTime < 0 && _meatCooker.RawMeatCount > 0)
            {
                _remainingCookingTime += 300;

                if (_remainingCookingTime is <= 0 and > -300) 
                    _timeCounter.SaveCurrentTime("MeatTime");

                _meatCooker.CookMeat(1);
            }

            if (_meatCooker.RawMeatCount > 0)
            {
                _remainingCookingTime -= Time.deltaTime;
            }
            else
            {
                _timeCounter.SaveCurrentTime("MeatTime");
            }

            _meatCookingTimerView.DisplayTimer(_meatCooker.RawMeatCount, _remainingCookingTime);
        }
    }
}