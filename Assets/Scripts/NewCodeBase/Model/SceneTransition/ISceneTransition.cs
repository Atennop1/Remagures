using Remagures.Root;

namespace Remagures.Model.SceneTransition
{
    public interface ISceneTransition : ILateUpdatable
    {
        bool HasActivated { get; }
        void Activate();
    }
}