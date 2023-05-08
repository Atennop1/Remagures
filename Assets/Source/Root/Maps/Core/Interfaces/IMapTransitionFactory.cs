using Remagures.Model.MapSystem;

namespace Remagures.Root
{
    public interface IMapTransitionFactory
    {
        IMapTransition Create();
    }
}