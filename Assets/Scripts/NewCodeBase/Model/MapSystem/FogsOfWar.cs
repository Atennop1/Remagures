using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.MapSystem
{
    public sealed class FogsOfWar
    {
        private readonly IReadOnlyDictionary<IMap, Texture2D> _fogs;

        public FogsOfWar(Dictionary<IMap, Texture2D> fogs)
            => _fogs = fogs ?? throw new ArgumentNullException(nameof(fogs));

        public Texture2D GetFor(IMap map)
            => _fogs[map];
    }
}