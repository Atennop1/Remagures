using Remagures.Model.SceneTransition;

namespace Remagures.Root
{
    public interface ISceneTransitionFactory
    {
        ISceneTransition Create();
    }
}