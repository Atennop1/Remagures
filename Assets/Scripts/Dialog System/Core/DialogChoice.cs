using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogChoice
{
    [field: SerializeField] public string ChoiceText { get; private set; }
    [field: SerializeField] public Dialog Dialog { get; private set; }
    [field: SerializeField] public UnityEvent OnChoiceEvent { get; private set; }
}
