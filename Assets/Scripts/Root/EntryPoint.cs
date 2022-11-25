using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public class EntryPoint : SerializedMonoBehaviour
    {
        [SerializeField] private List<CompositeRoot> _roots;
        private void Awake() => _roots.ForEach(root => root.Compose());
    }
}