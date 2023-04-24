using Remagures.Model.InventorySystem;

namespace Remagures.Tools
{
    public static class IItemExtensions
    {
        public static bool AreEquals(this IItem first, IItem second) =>
            first.Name == second.Name && first.Description == second.Description && first.Sprite == second.Sprite;
    }
}