﻿using System.Collections.Generic;
using System.Linq;
using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MapSelectorFactory : SerializedMonoBehaviour, IMapSelectorFactory
    {
        [SerializeField] private List<IMapFactory> _mapFactories;
        
        private AutoMapSelector _builtSelector;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        public IMapSelector Create()
        {
            if (_builtSelector != null)
                return _builtSelector;

            _builtSelector = new AutoMapSelector(_mapFactories.Select(factory => factory.Create()).ToList());
            _systemUpdate.Add(_builtSelector);
            return _builtSelector;
        }
    }
}