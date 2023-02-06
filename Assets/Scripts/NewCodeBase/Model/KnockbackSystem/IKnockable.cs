using UnityEngine;

namespace Remagures.Model.KnockbackSystem
{
    public interface IKnockable
    {
        public LayerMask InteractionMask { get; }
        public bool IsKnocked { get; }
        
        public void Knock(int knockTimeInMilliseconds);
    }
}
