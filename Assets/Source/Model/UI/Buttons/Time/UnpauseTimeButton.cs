using UnityEngine;

namespace Remagures.Model.UI
{
    public sealed class UnpauseTimeButton : IButton
    {
        public void Press()
            => Time.timeScale = 1;
    }
}