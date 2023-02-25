using System;

namespace Remagures.Model.MapSystem
{
    public sealed class MapTransition
    {
        public readonly IMap MapToTransit;

        public MapTransition(IMap mapToOpen)
            => MapToTransit = mapToOpen ?? throw new ArgumentNullException(nameof(mapToOpen));

        public void Transit()
        {
            if (MapToTransit.CanOpen())
                MapToTransit.Open();
        }
    }
}
