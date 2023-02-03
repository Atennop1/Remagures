using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.DialogSystem
{
    public class DialogBuilder : SerializedMonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private List<DialogLineBuilder> _lineBuilders;

        public Dialog BuiltDialog { get; private set; }

        public Dialog Build()
        {
            var builtLines = _lineBuilders.Select(builder => builder.BuiltLine).ToList();
            var result = new Dialog(_name, builtLines.ToArray());

            BuiltDialog = result;
            return result;
        }
    }
}