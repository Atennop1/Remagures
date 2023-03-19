using System;
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
        private readonly IMeatCooker _meatCooker;

        private float _remainingCookingTime;
        private bool _isLoopActive;

        public MeatCookingLoop(MeatCookingTimerView meatCookingTimerView, IMeatCooker meatCooker)
        {
            _meatCookingTimerView = meatCookingTimerView ?? throw new ArgumentNullException(nameof(meatCookingTimerView));
            _meatCooker = meatCooker ?? throw new ArgumentNullException(nameof(meatCooker));
        }

        public void Activate()
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
                _remainingCookingTime = 300;
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