using Remagures.Model.Flashing;

namespace Remagures.Root
{
    public interface IFlashingableFactory
    {
        IFlashingable Create();
    }
}