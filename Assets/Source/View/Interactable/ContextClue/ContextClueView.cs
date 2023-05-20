using UnityEngine;

namespace Remagures.View.Interactable
{
    public sealed class ContextClueView : MonoBehaviour, IContextClueView
    {
        [SerializeField] private GameObject _contextClueGameObject;

        public void DisplayQuestion()
            => _contextClueGameObject.SetActive(true);

        public void UnDisplay()
            => _contextClueGameObject.SetActive(false);
    }
}