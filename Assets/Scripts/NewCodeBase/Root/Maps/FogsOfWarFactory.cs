using System.Collections.Generic;
using System.Linq;
using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class FogsOfWarFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<IMapFactory, Texture2D> _fogs;
        private FogsOfWar _builtFogs;

        public FogsOfWar Create()
        {
            if (_builtFogs != null)
                return _builtFogs;

            _builtFogs = new FogsOfWar(_fogs.ToDictionary(pair => pair.Key.Create(), pair => pair.Value));
            return _builtFogs;
        }
    }
}