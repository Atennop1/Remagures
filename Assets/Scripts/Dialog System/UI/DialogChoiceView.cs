using UnityEngine;
using System;
using UnityEngine.UI;

public class DialogChoiceView : MonoBehaviour
{
    [SerializeField] private Text _choiceText;

    public int Index { get; private set; }
    public DialogChoice Choice { get; private set; }

    public void Setup(DialogChoice choice, int index)
    {
        _choiceText.text = choice.ChoiceText;
        Choice = choice;
        this.Index = index;
    }
}
