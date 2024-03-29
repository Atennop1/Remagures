﻿using System;
using BehaviorDesigner.Runtime;
using Remagures.Model.Input;

namespace Remagures.Model.Character.BehaviourTree
{
    [Serializable]
    public sealed class SharedMovementInput : SharedVariable<IMovementInput>
    {
        public static SharedMovementInput FromIMovementInput(IMovementInput value) 
        => new() { Value = value };
    }
}