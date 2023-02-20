using System;

namespace Remagures.Model.AI.NPC
{
    public readonly struct DudeStates
    {
        public readonly IState StayingState;
        public readonly IState TalkingState;
        public readonly IState WaitingState;
        public readonly IState WalkingState;

        public DudeStates(IState stayingState, IState talkingState, IState waitingState, IState walkingState)
        {
            StayingState = stayingState ?? throw new ArgumentNullException(nameof(stayingState));
            TalkingState = talkingState ?? throw new ArgumentNullException(nameof(talkingState));
            WaitingState = waitingState ?? throw new ArgumentNullException(nameof(waitingState));
            WalkingState = walkingState ?? throw new ArgumentNullException(nameof(walkingState));
        }
    }
}