using Remagures.SO.PlayerStuff;

namespace Remagures.Inventory.Abstraction
{
    public interface IRuneItem : IBaseItemComponent
    {
        public CharacterInfo CharacterInfo { get; }
        public RuneType RuneType { get; }
    }
}
