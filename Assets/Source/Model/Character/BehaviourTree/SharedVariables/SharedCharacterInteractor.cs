using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.Character.BehaviourTree
{
    [Serializable]
    public sealed class SharedCharacterInteractor : SharedVariable<ICharacterInteractor>
    {
        public static SharedCharacterInteractor FromICharacterInteractor(ICharacterInteractor value) 
            => new() { Value = value };
    }
}