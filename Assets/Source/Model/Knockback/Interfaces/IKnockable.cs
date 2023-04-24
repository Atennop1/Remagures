using UnityEngine;

namespace Remagures.Model.Knockback
{
    public interface IKnockable
    {
        LayerMask KnockingMask { get; }
        bool IsKnocking { get; }
        
        void Knock(Vector2 forceVector, int timeInMilliseconds);
        void StopKnocking();
    }
}
