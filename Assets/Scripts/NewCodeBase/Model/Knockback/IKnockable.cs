using UnityEngine;

namespace Remagures.Model.Knockback
{
    public interface IKnockable
    {
        public LayerMask InteractionMask { get; }
        public bool IsKnocking { get; }
        
        public void Knock(int knockTimeInMilliseconds);
    }
}
