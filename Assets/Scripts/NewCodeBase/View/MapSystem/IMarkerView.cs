using UnityEngine;

namespace Remagures.View.MapSystem
{
    public interface IMarkerView
    {
        void Display(Vector2 position);
        void UnDisplay();
    }
}