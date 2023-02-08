using UnityEngine;

namespace Remagures.Model.Flashing
{
    public interface IFlashingsStarter
    {
        void StartFlashing(Color flashColor, Color afterFlashColor);
    }
}