using System;
using Remagures.Tools;

namespace Remagures.Model.AI.NPC
{
    public struct RandomMovementData
    {
        public readonly float MinMovingTime;
        public readonly float MaxMovingTime;
        
        public readonly float MinStayingTime;
        public readonly float MaxStayingTime;

        public RandomMovementData(float minMoveTime, float maxMoveTime, float minWaitTime, float maxWaitTime)
        {
            MinMovingTime = minMoveTime.ThrowExceptionIfLessOrEqualsZero();
            MaxMovingTime = maxMoveTime.ThrowExceptionIfLessOrEqualsZero();
            
            MinStayingTime = minWaitTime.ThrowExceptionIfLessOrEqualsZero();
            MaxStayingTime = maxWaitTime.ThrowExceptionIfLessOrEqualsZero();
            
            if (MaxStayingTime < MinStayingTime)
                throw new ArgumentException("MaxWaitTime can't be less than MinWaitTime");
        }
    }
}