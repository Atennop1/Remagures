using UnityEngine;

namespace Remagures.Model.Knockback
{
    public interface IKnockable
    {
        LayerMask InteractionMask { get; }
        bool IsKnocking { get; }
        
        void Knock(int knockTimeInMilliseconds);
        void StopKnocking();
    }
}
