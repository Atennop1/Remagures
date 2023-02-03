using Remagures.SO;

namespace Remagures.Inventory
{
    public interface IRuneItem : IBaseItemComponent
    {
        public CharacterInfo CharacterInfo { get; }
        public RuneType RuneType { get; }
    }
}
