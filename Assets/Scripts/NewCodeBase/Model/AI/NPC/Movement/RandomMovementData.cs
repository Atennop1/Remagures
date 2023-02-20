using System;
using Remagures.Tools;

namespace Remagures.Model.AI.NPC
{
    public struct RandomMovementData
    {
        public readonly float MinMoveTime;
        public readonly float MaxMoveTime;
        
        public readonly float MinWaitTime;
        public readonly float MaxWaitTime;

        public RandomMovementData(float minMoveTime, float maxMoveTime, float minWaitTime, float maxWaitTime)
        {
            MinMoveTime = minMoveTime.ThrowExceptionIfLessOrEqualsZero();
            MaxMoveTime = maxMoveTime.ThrowExceptionIfLessOrEqualsZero();

            if (MaxMoveTime < MinMoveTime)
                throw new ArgumentException("MaxMoveTime can't be less than MinMoveTime");
            
            MinWaitTime = minWaitTime.ThrowExceptionIfLessOrEqualsZero();
            MaxWaitTime = maxWaitTime.ThrowExceptionIfLessOrEqualsZero();
            
            if (MaxWaitTime < MinWaitTime)
                throw new ArgumentException("MaxWaitTime can't be less than MinWaitTime");
        }
    }
}