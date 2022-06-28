using UnityEngine;

public class ContextClue : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _contextClue;
    private bool _contextActive;

    public void ChangeContext()
    {
        _contextActive = _player.PlayerInteract.CurrentInteractable != null && _player.PlayerInteract.CanShowContextClue;
        _contextClue.SetActive(_contextActive);
    }
}
