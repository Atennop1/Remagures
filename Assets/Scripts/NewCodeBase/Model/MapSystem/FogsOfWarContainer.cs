using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class FogsOfWarContainer
    {
        private readonly Dictionary<IMap, Texture2D> _fogs;

        public FogsOfWarContainer(Dictionary<IMap, Texture2D> fogs)
            => _fogs = fogs ?? throw new ArgumentNullException(nameof(fogs));

        public Texture2D Get(IMap map)
            => _fogs[map];
    }
}