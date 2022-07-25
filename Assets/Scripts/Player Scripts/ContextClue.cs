using UnityEngine;

public class ContextClue : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _contextClue;
    private bool _contextActive;

    public void ChangeContext()
    {
        _contextActive = _player.PlayerInteracting.CurrentInteractable != null && _player.PlayerInteracting.CanShowContextClue;
        _contextClue.SetActive(_contextActive);
    }
}
