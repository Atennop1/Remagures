using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    public bool CanAttack => AttackCoroutine == null;
    public Coroutine AttackCoroutine;

    public void AttackMethod()
    {
        if (_player.PlayerInteract.CurrentState == InteractingState.Ready)
        {
            _player.PlayerInteract.Interact();
            return;
        }
            
        if (AttackCoroutine == null && _player.CurrentState != PlayerState.Attack && _player.UniqueManager.WeaponSlot.ThisItem.ItemData.ItemName != "")
            AttackCoroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _player.CurrentState = PlayerState.Attack;

        _player.PlayerAnimations.ChangeAnim("attacking", true);
        yield return null;

        _player.PlayerAnimations.ChangeAnim("attacking", false);
        yield return new WaitForSeconds(0.33f);

        if (_player.CurrentState != PlayerState.Interact)
            _player.CurrentState = PlayerState.Idle;

        AttackCoroutine = null;
    }
    
    public void MagicAttackMethod()
    {
        if ((_player.UniqueManager.MagicSlot.ThisItem as MagicInventoryItem) != null && (_player.UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).MagicItemData.ThisEvent != null)
            (_player.UniqueManager.MagicSlot.ThisItem as MagicInventoryItem).MagicItemData.ThisEvent.Invoke();
    }

    public void MagicAttackCreateCoroutine(IEnumerator method)
    {
        AttackCoroutine = StartCoroutine(method);
    }
}
