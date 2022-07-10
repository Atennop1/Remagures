using UnityEngine;

public class MagicReaction : MonoBehaviour
{
    [SerializeField] private Signal _magicSignal;
    
    public void Use(int amountToIncrease)
    {
        for (int i = 0; i < amountToIncrease; i++)
            _magicSignal.Invoke();
    }
}
