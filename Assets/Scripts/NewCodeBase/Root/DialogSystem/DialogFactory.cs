using System.Collections.Generic;
using System.Linq;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class DialogFactory : SerializedMonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private List<DialogLineFactory> _lineBuilders;

        private Dialog _builtDialog;

        public Dialog Build()
        {
            if (_builtDialog != null)
                return _builtDialog;

            var builtLines = _lineBuilders.Select(builder => builder.BuiltLine).ToList();
            var result = new Dialog(_name, builtLines.ToArray());

            _builtDialog = result;
            return _builtDialog;
        }
    }
}