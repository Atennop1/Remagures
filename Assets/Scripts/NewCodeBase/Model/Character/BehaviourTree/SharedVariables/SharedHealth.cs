using BehaviorDesigner.Runtime;
using Remagures.Model.Health;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class SharedHealth : SharedVariable<IHealth>
    {
        public static SharedHealth FromIHealth(IHealth value) 
            => new() { Value = value };
    }
}