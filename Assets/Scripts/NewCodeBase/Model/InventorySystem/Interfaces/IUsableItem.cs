namespace Remagures.Model.InventorySystem
{
    public interface IUsableItem : IItem
    {
        bool HasUsed { get; }
        void Use();
    }
}
