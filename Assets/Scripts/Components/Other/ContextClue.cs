using Remagures.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.Components
{
    public class ContextClue : MonoBehaviour
    {
        [FormerlySerializedAs("_playerInteracting")] [SerializeField] private PlayerInteractingHandler _playerInteractingHandler;
        [SerializeField] private GameObject _contextClue;
        private bool _contextActive;

        public void ChangeContext()
        {
            _contextActive = _playerInteractingHandler.CurrentInteractable != null && _playerInteractingHandler.CanShowContextClue;
            _contextClue.SetActive(_contextActive);
        }
    }
}
