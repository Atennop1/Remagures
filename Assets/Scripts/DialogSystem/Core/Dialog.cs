using System.Collections.Generic;
using UnityEngine;

namespace Remagures.DialogSystem.Core
{
    [CreateAssetMenu(menuName = "Dialog System/Dialog")]
    public class Dialog : ScriptableObject
    {
        public IReadOnlyList<DialogLine> Lines => _lines;
        [NonReorderable] [SerializeField] private List<DialogLine> _lines;

        public IReadOnlyList<DialogChoice> Choices => _choices;
        [SerializeField] [NonReorderable] private List<DialogChoice> _choices;
    }
}
