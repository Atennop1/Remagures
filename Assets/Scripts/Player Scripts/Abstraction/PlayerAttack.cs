using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Player _player;
    public bool CanAttack => _attackCoroutine == null;
    private Coroutine _attackCoroutine;

    private const string ATTACK_ANIMATOR_NAME = "attacking";

    public void AttackMethod()
    {
        if (_player.PlayerInteract.CurrentState == InteractingState.Ready)
        {
            _player.PlayerInteract.Interact();
            return;
        }
            
        if (_attackCoroutine == null && _player.CurrentState != PlayerState.Attack && _player.UniqueSetup.WeaponSlot.ThisCell.Item.ItemName != "")
            _attackCoroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _player.ChangeState(PlayerState.Attack);

        _player.PlayerAnimations.ChangeAnim(ATTACK_ANIMATOR_NAME, true);
        yield return null;

        _player.PlayerAnimations.ChangeAnim(ATTACK_ANIMATOR_NAME, false);
        yield return new WaitForSeconds(0.33f);

        if (_player.CurrentState != PlayerState.Interact)
            _player.ChangeState(PlayerState.Idle);

        _attackCoroutine = null;
    }
    
    public void MagicAttackMethod()
    {
        (_player.UniqueSetup.MagicSlot.ThisCell.Item as IMagicItem)?.ThisEvent?.Invoke();
    }

    public void StartMagicAttackCoroutine(IEnumerator method)
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
        _attackCoroutine = StartCoroutine(method);
    }

    public void SetAttackCoroutineToNull()
    {
        _attackCoroutine = null;
    }
}
