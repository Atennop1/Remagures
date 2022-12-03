using UnityEngine;

namespace Remagures.Components.Other
{
    public class SceneTransitionsEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _fadeOutPanel;
        
        private void Awake()
        {
            if (_fadeOutPanel == null) 
                return;
        
            var panel = Instantiate(_fadeOutPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }
}