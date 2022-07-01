using System.Collections;
using UnityEngine;

public class PlayerKnockable : MonoBehaviour, IKnockable
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Signal _cameraKick;
    [field: SerializeField] public LayerMask LayerMask { get; private set;}

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        if (_player.CurrentState != PlayerState.Stagger)
        {
            _player.ChangeState(PlayerState.Stagger);
            StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
        }
    }

    public IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime)
    {
        _cameraKick.Raise();
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            _player.ChangeState(PlayerState.Idle);
        }
    }
}
