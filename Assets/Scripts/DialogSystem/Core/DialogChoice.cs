using UnityEngine;
using UnityEngine.Events;

namespace Remagures.DialogSystem.Core
{
    [System.Serializable]
    public struct DialogChoice
    {
        [field: SerializeField] public string ChoiceText { get; private set; }
        [field: SerializeField] public Dialog Dialog { get; private set; }
        [field: SerializeField] public UnityEvent OnChoiceEvent { get; private set; }
    }
}
