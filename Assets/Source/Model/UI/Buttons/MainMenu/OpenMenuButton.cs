using UnityEngine.SceneManagement;

namespace Remagures.Model.UI
{
    public sealed class OpenMenuButton : IButton
    {
        public void Press()
            => SceneManager.LoadScene("Menu");
    }
}