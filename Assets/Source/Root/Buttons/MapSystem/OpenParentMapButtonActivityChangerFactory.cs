using System.Collections.Generic;
using System.Linq;
using Remagures.Model.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Root
{
    public sealed class OpenParentMapButtonActivityChangerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private List<IMapFactory> _mapFactories;
        [SerializeField] private ParentMapOpenerFactory _parentMapOpenerFactory;
        [SerializeField] private Button _button;
        
        private readonly SystemUpdate _systemUpdate = new();

        public void Awake()
        {
            var changer = new OpenParentMapButtonActivityChanger(_mapFactories.Select(factory => factory.Create()).ToList(), _parentMapOpenerFactory.Create(), _button);
            _systemUpdate.Add(changer);
        }

        private void Update()
            => _systemUpdate.UpdateAll();
    }
}