using UnityEngine;

public class ContextClue : MonoBehaviour
{
    [SerializeField] private PlayerInteracting _playerInteracting;
    [SerializeField] private GameObject _contextClue;
    private bool _contextActive;

    public void ChangeContext()
    {
        _contextActive = _playerInteracting.CurrentInteractable != null && _playerInteracting.CanShowContextClue;
        _contextClue.SetActive(_contextActive);
    }
}
