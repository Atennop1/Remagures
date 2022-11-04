using System.Collections;
using Remagures.Components.Base;
using Remagures.SO.Other;
using UnityEngine;

namespace Remagures.Player.Components
{
    public class PlayerKnockable : MonoBehaviour, IKnockable, IPlayerComponent
    {
        [SerializeField] private Player _player;
        [SerializeField] private Signal _cameraKick;
        [field: SerializeField] public LayerMask InteractionMask { get; private set;}
        public bool IsKnocked { get; private set; }

        public void Knock(Rigidbody2D thisRigidbody, float knockTime)
        {
            if (_player.CurrentState == PlayerState.Stagger) return;
        
            _player.ChangeState(PlayerState.Stagger);
            StartCoroutine(KnockCoroutine(thisRigidbody, knockTime));
        }

        private IEnumerator KnockCoroutine(Rigidbody2D thisRigidbody, float knockTime)
        {
            IsKnocked = true;
            _cameraKick.Invoke();
            
            if (thisRigidbody == null) yield break;
            yield return new WaitForSeconds(knockTime);
            
            thisRigidbody.velocity = Vector2.zero;
            _player.ChangeState(PlayerState.Idle);
            IsKnocked = false;
        }
    }
}
