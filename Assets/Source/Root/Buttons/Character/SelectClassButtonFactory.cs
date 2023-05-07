using Remagures.Model.Buttons;
using Remagures.Model.Character;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class SelectClassButtonFactory : SerializedMonoBehaviour, IButtonFactory
    {
        [SerializeField] private CharacterClass _selectingClass;
        private IButton _builtButton;
        
        public IButton Create()
        {
            if (_builtButton != null)
                return _builtButton;

            _builtButton = new SelectClassButton(new BinaryStorage<CharacterClass>(new Path("CharacterClass")), _selectingClass);
            return _builtButton;
        }
    }
}