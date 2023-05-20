using UnityEngine;

namespace Remagures.View.MapSystem
{
    public interface ICharacterMarkerView
    {
        void Display(Vector2 position);
        void UnDisplay();
    }
}