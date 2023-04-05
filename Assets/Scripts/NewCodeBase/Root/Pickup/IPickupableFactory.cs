using Remagures.Model.Pickup;

namespace Remagures.Root
{
    public interface IPickupableFactory
    {
        IPickupable Create();
    }
}