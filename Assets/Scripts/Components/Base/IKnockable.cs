using UnityEngine;

namespace Remagures.Components.Base
{
    public interface IKnockable
    {
        public LayerMask InteractionMask { get; }
        public void Knock(Rigidbody2D rigidbody, float knockTime);
        public bool IsKnocked { get; }
    }
}
