using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class CurrentDialogPlayingFactory : SerializedMonoBehaviour, IAdditionalBehaviourFactory
    {
        [SerializeField] private IDialogsListFactory _dialogsListFactory;
        [SerializeField] private IDialogPlayerFactory _dialogPlayerFactory;
        private IAdditionalBehaviour _builtBehaviour;
        
        public IAdditionalBehaviour Create()
        {
            if (_builtBehaviour != null)
                    return _builtBehaviour;
            
            _builtBehaviour = new NextDialogPlaying(_dialogPlayerFactory.Create(), _dialogsListFactory.Create());
            return _builtBehaviour;
        }
    }
}