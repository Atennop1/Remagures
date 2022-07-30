using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_canExplore)
        {
            Vector2 position = _currentMap.CalculatePositionOnTexture();
            Tex2DExtension.DrawCircle(_mapTexture, new Color(0, 0, 0, 0.5f), (int)position.x, (int)position.y, 50);
            Tex2DExtension.DrawCircle(_mapTexture, new Color(0, 0, 0, 0), (int)position.x, (int)position.y, 47);
            StartCoroutine(ExploreCooldownCoroutine());
            _canExplore = false;
        }
    }

    private void Start()
    {
        _canExplore = true;
        _currentMap = _setup.CurrentLocationMap;
        _mapTexture = (Texture2D)_currentMap.ExplorationTexture;
        _currentMap.Init(_view, _goalsView, transform);
        StartCoroutine(TextureApplyCoroutine());
    }

    private IEnumerator TextureApplyCoroutine()
    {
        while (true)
        {
            yield return null;
            if (_canExplore)
            {
                Explore();
                _mapTexture.Apply();
            }
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
    public static void DrawCircle(this Texture2D texture, Color color, int x, int y, int radius = 3)
    {
        float rSquared = radius * radius;
        Color[] pixels = texture.GetPixels();

        for (int u = x - radius; u < x + radius + 1; u++)
            for (int v = y - radius; v < y + radius + 1; v++)
                if (u > 0 && v > 0 && u < texture.width && v < texture.height && 
                        (x - u) * (x - u) + (y - v) * (y - v) < rSquared && 
                        texture.GetPixel(u, v).a > color.a)
                    pixels[v * texture.width + u] = color;
        
        texture.SetPixels(pixels);
    }
}