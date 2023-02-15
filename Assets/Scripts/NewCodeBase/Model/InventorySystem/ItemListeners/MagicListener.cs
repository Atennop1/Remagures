using Remagures.Model.Magic;
using Remagures.Root;

namespace Remagures.Model.InventorySystem
{
    public class MagicListener : IUpdatable
    {
        private readonly IUsableItem _usableItem;
        private readonly IMagicApplier _magicApplier;
        private readonly IMagic _magic;

        public void Update()
        {
            if (_usableItem.HasUsed)
                _magicApplier.Use(_magic);
        }
    }
}