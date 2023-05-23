using BehaviorDesigner.Runtime;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class SharedMagicApplier : SharedVariable<CharacterMagicApplier>
    {
        public static SharedMagicApplier FromCharacterMagicApplier(CharacterMagicApplier value) 
            => new() { Value = value };
    }
}