using BehaviorDesigner.Runtime;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class SharedCharacterAttacker : SharedVariable<CharacterAttacker>
    {
        public static implicit operator SharedCharacterAttacker(CharacterAttacker value) 
            => new() { Value = value };
    }
}