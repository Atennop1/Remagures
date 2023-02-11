using Remagures.SO;

namespace Remagures.Model.InventorySystem
{
    public interface IRuneItem : IBaseItemComponent
    {
        public CharacterInfo CharacterInfo { get; }
        public RuneType RuneType { get; }
    }
}
