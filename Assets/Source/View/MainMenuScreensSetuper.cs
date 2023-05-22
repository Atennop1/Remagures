using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View
{
    public sealed class MainMenuScreensSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject _newGameMenu;
        [SerializeField] private GameObject _oldGameMenu;

        private readonly ISaveStorage<bool> _isNewGameStorage = new BinaryStorage<bool>(new Path("IsNewGameStarted"));

        private void Awake()
        {
            if (!_isNewGameStorage.HasSave() || _isNewGameStorage.Load())
            {
                _newGameMenu.SetActive(true);
                _oldGameMenu.SetActive(false);
            }
            else
            {
                _newGameMenu.SetActive(false);
                _oldGameMenu.SetActive(true);
            }
        }
    }
}
