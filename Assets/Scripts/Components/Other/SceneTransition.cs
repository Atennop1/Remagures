using System.Collections;
using System.Collections.Generic;
using Remagures.Cutscenes;
using Remagures.Interactable;
using Remagures.Player;
using Remagures.Root;
using Remagures.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remagures.Components
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private VectorValue _playerPositionStorage;
        [SerializeField] private VectorValue _playerViewDirectionStorage;
        
        [Space]
        [SerializeField] private UIActivityChanger _uiActivityChanger;
        [SerializeField] private GameObject _fadeInPanel;
        [SerializeField] private SmoothColorSwitcher _colorSwitcher;
        
        [Space]
        [SerializeField] private Vector2 _playerPosition;
        [SerializeField] private Vector2 _playerViewDirection;
        private ISystemUpdate _systemUpdate;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Player.Player player) || collision.isTrigger) 
                return;

            StartTransitionCutscene(player);
            StartCoroutine(FadeCoroutine());
            
            _playerPositionStorage.SetValue(_playerPosition);
            _playerViewDirectionStorage.SetValue(_playerViewDirection);
        }

        private void StartTransitionCutscene(Player.Player player)
        {
            var playerMovement = player.GetPlayerComponent<PlayerMovement>();
            _systemUpdate = new SystemUpdate();
            
            var transitingCutscene = new Cutscene(new List<ICutsceneAction>
            {
                new StartAction(_uiActivityChanger),
                new MoveAction(playerMovement, player.transform.position + (Vector3)playerMovement.PlayerViewDirection * 2)
            });
            
            _colorSwitcher?.SwitchTo(Color.black, 0.5f);
            _systemUpdate.Add(transitingCutscene);
            transitingCutscene.Start();
        }

        private IEnumerator FadeCoroutine()
        {
            if (_fadeInPanel != null)
                Instantiate(_fadeInPanel, Vector3.zero, Quaternion.identity);

            yield return new WaitForSeconds(1.5f);
            var asyncOperation = SceneManager.LoadSceneAsync(_sceneToLoad);

            while (!asyncOperation.isDone)
                yield return null;
        }
        
        private void FixedUpdate() => _systemUpdate?.UpdateAll();
    }
}
