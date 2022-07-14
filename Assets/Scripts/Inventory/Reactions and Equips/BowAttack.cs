using System.Collections;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    [SerializeField] private FloatValue _currentMagic;
    [SerializeField] private GameObject _projectile;
    private Player _player;
    
    public void MagicAttackMethod()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player.PlayerAttack.CanAttack && _player.CurrentState != PlayerState.Attack && _player.PlayerInteract.CurrentState != InteractingState.Interact)
            _player.PlayerAttack.StartMagicAttackCoroutine(MagicAttack());
    }

    private IEnumerator MagicAttack()
    {
        if (_player.PlayerAnimations.PlayerAnimator.GetBool("attacking") != true && CheckArrowValid())
        {
            _player.ChangeState(PlayerState.Attack);
            
            yield return null;
            MakeArrow();
            yield return new WaitForSeconds(_player.ClassStat.BowReloadTime);

            if (_player.CurrentState != PlayerState.Interact)
                _player.ChangeState(PlayerState.Idle);

            _player.PlayerAttack.SetAttackCoroutineToNull();
        }
    }

    private void MakeArrow()
    {
        if (_currentMagic.Value >= _player.MagicCount.MagicCost)
        {
            _player.DecreaseMagicSignal.Invoke();
            Vector2 temp = new Vector2(_player.PlayerAnimations.PlayerAnimator.GetFloat("moveX"), _player.PlayerAnimations.PlayerAnimator.GetFloat("moveY"));
            Arrow arrow = Instantiate(_projectile, _player.gameObject.transform.position, Quaternion.identity).GetComponent<Arrow>();

            arrow.Setup(temp, ChooseArrowDirection());
            arrow.transform.position += arrow.transform.right * 0.5f;
            arrow.GetComponent<PlayerDamage>().Init(_player);
        }
    }

    private bool CheckArrowValid()
    {
        if (_projectile.TryGetComponent<Arrow>(out Arrow arrow))
        {
            RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, new Vector2(_player.PlayerAnimations.PlayerAnimator.GetFloat("moveX"), _player.PlayerAnimations.PlayerAnimator.GetFloat("moveY")), 1, 3);
            return hit.collider == null;
        }

        return false;
    }
    
    private Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(_player.PlayerAnimations.PlayerAnimator.GetFloat("moveY"), _player.PlayerAnimations.PlayerAnimator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
}
