using Remagures.Model.MeatSystem;

namespace Remagures.Root.MeatSystem
{
    public interface IMeatCookerFactory
    {
        IMeatCooker Create();
    }
}