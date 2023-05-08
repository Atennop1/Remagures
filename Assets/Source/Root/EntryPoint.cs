using Remagures.Root.Dialogs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class EntryPoint : SerializedMonoBehaviour
    {
        [SerializeField] private IDialogsListFactory _dialogsListFactory;
        [SerializeField] private GridsFactory _gridsFactory;
        [SerializeField] private CutsceneFactory cutsceneFactory;
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            _dialogsListFactory.Create();
            _gridsFactory.Create();
            cutsceneFactory.Create();
        }
    }
}