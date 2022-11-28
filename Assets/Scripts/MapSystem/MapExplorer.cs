using System.Collections;
using Cysharp.Threading.Tasks;
using Remagures.MapSystem.Maps;
using Remagures.QuestSystem;
using UnityEngine;

namespace Remagures.MapSystem
{
    public class MapExplorer : MonoBehaviour
    {
        [SerializeField] private MapView _view;
        [SerializeField] private QuestGoalsView _goalsView;
        [SerializeField] private MapSetup _setup;

        private Texture2D _mapTexture;
        private LocationMap _currentMap;
        
        private bool _canExplore;

        public void Explore()
        {
            if (!_canExplore) return;
            
            var position = _currentMap.CalculatePositionOnTexture();
            _mapTexture.DrawCircle(new Color(0, 0, 0, 0.5f), (int)position.x, (int)position.y, 50);
            _mapTexture.DrawCircle(new Color(0, 0, 0, 0), (int)position.x, (int)position.y, 47);
                
            StartCoroutine(ExploreCooldownCoroutine());
            _canExplore = false;
        }

        private void Start()
        {
            _canExplore = true;
            _currentMap = _setup.CurrentLocationMap;
            _mapTexture = _currentMap.ExplorationTexture;
            
            _currentMap.Init(_view, _goalsView, transform);
            StartCoroutine(TextureApplyCoroutine());
        }

        private IEnumerator TextureApplyCoroutine()
        {
            while (true)
            {
                yield return null;
                if (!_canExplore) continue;
            
                Explore();
                _mapTexture.Apply();
            }
        }

        private IEnumerator ExploreCooldownCoroutine()
        {
            yield return new WaitForSeconds(0.2f);
            _canExplore = true;
        }
    }

    public static class Tex2DExtension
    {
        public static UniTask DrawCircle(this Texture2D texture, Color color, int x, int y, int radius = 3)
        {
            float rSquared = radius * radius;
            var pixels = texture.GetPixels32();

            var textureWidth = texture.width;
            var textureHeight = texture.height;
            var colorA = color.a;

            for (var u = x - radius; u < x + radius + 1; u++)
                for (var v = y - radius; v < y + radius + 1; v++)
                    if (u > 0 && v > 0 && u < textureWidth && v < textureHeight && (x - u) * (x - u) + (y - v) * (y - v) < rSquared && pixels[v * textureWidth + u].a > colorA)
                        pixels[v * textureWidth + u] = color;

            texture.SetPixels32(pixels);
            return UniTask.CompletedTask;
        }
    }
}