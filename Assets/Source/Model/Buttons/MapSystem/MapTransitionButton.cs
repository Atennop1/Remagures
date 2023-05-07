using System;
using Remagures.Model.MapSystem;

namespace Remagures.Model.Buttons
{
    public sealed class MapTransitionButton : IButton
    {
        private readonly IMapTransition _mapTransition;

        public MapTransitionButton(IMapTransition mapTransition) 
            => _mapTransition = mapTransition ?? throw new ArgumentNullException(nameof(mapTransition));

        public void Press() 
            => _mapTransition.Transit();
    }
}