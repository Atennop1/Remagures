using UnityEngine.SceneManagement;

namespace Remagures.Model.Buttons
{
    public sealed class OpenMenuButton : IButton
    {
        public void Press()
            => SceneManager.LoadScene("Menu");
    }
}