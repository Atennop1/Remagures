using UnityEngine;
using UnityEngine.UI;

public class DialogChoice : MonoBehaviour
{
    [SerializeField] private Text _choiceText;
    public int Index { get; private set; }

    public void Setup(string text, int index)
    {
        _choiceText.text = text;
        this.Index = index;
    }
}
