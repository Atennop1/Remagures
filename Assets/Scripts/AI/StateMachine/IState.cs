﻿namespace Remagures.AI
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}