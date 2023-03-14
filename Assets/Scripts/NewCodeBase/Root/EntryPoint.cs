using Remagures.Root.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class EntryPoint : SerializedMonoBehaviour
    {
        [SerializeField] private DialogsListFactory _dialogsListFactory;
        [SerializeField] private GridsFactory _gridsFactory;
        [SerializeField] private TestCutsceneFactory _testCutsceneFactory;
        
        private void Awake()
        {
            _dialogsListFactory.Create();
            _gridsFactory.Create();
            _testCutsceneFactory.Create();
        }
    }
}