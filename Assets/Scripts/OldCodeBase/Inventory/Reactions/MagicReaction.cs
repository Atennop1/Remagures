using Remagures.SO;
using UnityEngine;

namespace Remagures.Inventory
{
    public class MagicReaction : MonoBehaviour
    {
        [SerializeField] private Signal _magicSignal;
    
        public void Use(int amountToIncrease)
        {
            for (var i = 0; i < amountToIncrease; i++)
                _magicSignal.Invoke();
        }
    }
}
