using Remagures.QuestSystem;
using UnityEngine;

namespace Remagures.MapSystem
{
    public class LocationMap : Map
    {
        [field: SerializeField, Header("Location Stuff")] public Texture2D ExplorationTexture { get; private set; }
        [SerializeField] private PlayerMarker _playerMarker;

        [Space]
        [SerializeField] private Vector2 _worldSize;
        [SerializeField] private Vector2 _worldOffset;

        [Space]
        [SerializeField] private Vector3 _playerMarkerScale;

        private Transform _player;

        public void Init(MapView view, QuestGoalsView goalsView, Transform player)
        {
            base.Init(view, goalsView);
            _player = player;
        }

        protected override void DisplayPlayerMarker()
        {
            if (_player == null)
                return;

            _playerMarker.TryDisplay(MapImageTransform, _playerMarkerScale, this);
            _playerMarker.SetPosition(_player.transform.position * (MapImageTransform.sizeDelta / _worldSize));
        }

        public Vector2 CalculatePositionOnTexture()
        {
            return (_player.transform.position + (Vector3)(_worldSize / 2 + _worldOffset)) * 
                   (new Vector2(ExplorationTexture.width, ExplorationTexture.height) / _worldSize);
        }
    }
}
