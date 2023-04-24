using System.Collections.Generic;
using Remagures.Model.MapSystem;
using Remagures.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class IsMapVisitedFactory : SerializedMonoBehaviour, IIsMapVisitedFactory
    {
        [SerializeField] private List<SceneData> _scenes;
        private IIsMapVisited _builtCondition;
        
        public IIsMapVisited Create()
        {
            if (_builtCondition != null)
                return _builtCondition;

            _builtCondition = new IsMapVisited(_scenes);
            return _builtCondition;
        }
    }
}