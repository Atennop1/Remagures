using Remagures.Model.MapSystem;

namespace Remagures.Root
{
    public interface IMapExplorerFactory
    {
        IMapExplorer Create();
    }
}