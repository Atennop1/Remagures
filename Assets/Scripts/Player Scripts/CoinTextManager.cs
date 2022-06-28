using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    [SerializeField] private FloatValue _numberOfCoins;
    [SerializeField] private TextMeshProUGUI _coinDisplay;
    
    public void Start()
    {
        _coinDisplay.text = _numberOfCoins.Value.ToString();
    }
}
