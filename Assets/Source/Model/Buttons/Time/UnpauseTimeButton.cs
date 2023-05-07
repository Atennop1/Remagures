using UnityEngine;

namespace Remagures.Model.Buttons
{
    public sealed class UnpauseTimeButton : IButton
    {
        public void Press()
            => Time.timeScale = 1;
    }
}