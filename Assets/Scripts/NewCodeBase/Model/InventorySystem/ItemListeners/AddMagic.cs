using Remagures.SO;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public class AddMagic //TODO redesign this after magic
    {
        private Signal _magicSignal;
    
        public void Use(int amountToIncrease)
        {
            for (var i = 0; i < amountToIncrease; i++)
                _magicSignal.Invoke();
        }
    }
}
