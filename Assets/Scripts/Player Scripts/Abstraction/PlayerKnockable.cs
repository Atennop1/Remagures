using System.Collections;
using UnityEngine;

public class PlayerKnockable : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    public Signal _cameraKick;

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
    }

    public IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime)
    {
        _cameraKick.Raise();
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            _player.CurrentState = PlayerState.Idle;
        }
    }
}
