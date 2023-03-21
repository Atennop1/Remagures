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
        [SerializeField] private List<IDialogLineFactory> _lineFactories;

        private Dialog _builtDialog;

        public Dialog Create()
        {
            if (_builtDialog != null)
                return _builtDialog;

            var builtLines = _lineFactories.Select(builder => builder.Create()).ToList();
            _builtDialog = new Dialog(_name, builtLines.ToArray());
            return _builtDialog;
        }
    }
}