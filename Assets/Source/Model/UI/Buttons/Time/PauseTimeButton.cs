using UnityEngine;

namespace Remagures.Model.UI
{
    public sealed class PauseTimeButton : IButton
    {
        public void Press()
            => Time.timeScale = 0;
    }
}