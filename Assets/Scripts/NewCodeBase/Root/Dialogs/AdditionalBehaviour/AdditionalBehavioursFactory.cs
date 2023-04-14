using System.Collections.Generic;
using System.Linq;
using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class AdditionalBehavioursFactory : SerializedMonoBehaviour, IAdditionalBehaviourFactory
    {
        [SerializeField] private List<IAdditionalBehaviourFactory> _additionalBehaviourFactories;
        private IAdditionalBehaviour _builtBehaviour;

        public IAdditionalBehaviour Create()
        {
            if (_builtBehaviour != null)
                return _builtBehaviour;
            
            _builtBehaviour = new AdditionalBehaviours(_additionalBehaviourFactories.Select(factory => factory.Create()).ToList());
            return _builtBehaviour;
        }
    }
}