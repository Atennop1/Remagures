using Remagures.Model.Flashing;
using Remagures.Model.Health;

namespace Remagures.Model.Attacks
{
    public interface ITarget
    {
        IHealth Health { get; }
        IFlashingable Flashingable { get; }
    }
}