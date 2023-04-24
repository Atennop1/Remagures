using System;

namespace Remagures.Model.MapSystem
{
    public sealed class MapTransition : IMapTransition
    {
        public IMap MapToTransit { get; }

        public MapTransition(IMap mapToOpen)
            => MapToTransit = mapToOpen ?? throw new ArgumentNullException(nameof(mapToOpen));

        public void Transit()
        {
            if (MapToTransit.CanOpen())
                MapToTransit.Open();
        }
    }
}
