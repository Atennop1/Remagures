using Remagures.Model.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Root
{
    public sealed class OpenParentMapButtonActivityChangerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ParentMapOpenerFactory _parentMapOpenerFactory;
        private OpenParentMapButtonActivityChanger _builtChanger;

        public OpenParentMapButtonActivityChanger Create()
        {
            if (_builtChanger != null)
                return _builtChanger;

            _builtChanger = new OpenParentMapButtonActivityChanger(_button, _parentMapOpenerFactory.Create());
            return _builtChanger;
        }
    }
}