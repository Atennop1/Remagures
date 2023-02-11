using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        Sprite Sprite { get; }
        bool IsStackable { get; }
    }
}
