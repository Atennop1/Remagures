using Remagures.Model.MeatSystem;

namespace Remagures.Root
{
    public interface IMeatCookerFactory
    {
        IMeatCooker Create();
    }
}