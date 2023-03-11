using System;
using Remagures.Model.Magic;
using Remagures.Root;

namespace Remagures.Model.InventorySystem
{
    public class MagicListener : IUpdatable
    {
        private readonly IUsableItem _usableItem;
        private readonly IMagicApplier _magicApplier;
        private readonly IMagic _magic;

        public MagicListener(IUsableItem usableItem, IMagicApplier magicApplier, IMagic magic)
        {
            _usableItem = usableItem ?? throw new ArgumentNullException(nameof(usableItem));
            _magicApplier = magicApplier ?? throw new ArgumentNullException(nameof(magicApplier));
            _magic = magic ?? throw new ArgumentNullException(nameof(magic));
        }

        public void Update()
        {
            if (_usableItem.HasUsed)
                _magicApplier.Use(_magic);
        }
    }
}