using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.DialogSystem
{
    public class DialogBuilder : SerializedMonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private List<DialogLineBuilder> _lineBuilders;

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