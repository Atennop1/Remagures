using System;
using Remagures.Root;
using Remagures.Tools;
using Remagures.View.MeatSystem;
using SaveSystem;
using SaveSystem.Paths;
using UnityEngine;

namespace Remagures.Model.MeatSystem
{
    public class MeatCookingLoop : IUpdatable
    {
        private readonly TimeDifferenceCounter _timeDifferenceCounter = new(new BinaryStorage<DateTime>(new Path("RemainingMeatCookingTime")));
        private readonly IMeatCookingTimerView _meatCookingTimerView;
        private readonly IMeatCooker _meatCooker;

        private bool _hasRawMeat => _meatCooker.RawMeatCount > 0;
        private float _remainingCookingTime;

        public MeatCookingLoop(IMeatCookingTimerView meatCookingTimerView, IMeatCooker meatCooker)
        {
            _meatCookingTimerView = meatCookingTimerView ?? throw new ArgumentNullException(nameof(meatCookingTimerView));
            _meatCooker = meatCooker ?? throw new ArgumentNullException(nameof(meatCooker));
        }

        public void Activate()
        {
            _remainingCookingTime = 300;

            if (_hasRawMeat)
                _remainingCookingTime -= _timeDifferenceCounter.GetTimeDifference();
        }

        public void Update()
        {
            if (!_hasRawMeat)
            {
                _remainingCookingTime = 300;
                return;
            }
            
            while (_remainingCookingTime < 0 && _hasRawMeat)
            {
                _remainingCookingTime += 300;

                if (_remainingCookingTime is <= 0 and > -300) 
                    _timeDifferenceCounter.SaveCurrentTime();
                
                _meatCooker.CookMeat(1);
            }
            
            _meatCookingTimerView.DisplayTimer(_remainingCookingTime);

            if (_hasRawMeat)
            {
                _remainingCookingTime -= Time.deltaTime;
                return;
            }
            
            _timeDifferenceCounter.SaveCurrentTime();
        }
    }
}