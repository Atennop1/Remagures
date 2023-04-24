namespace Remagures.Model.MapSystem
{
    public sealed class IsCharacterMarkerContainsInMap : IIsMarkerContainsInMap
    {
        public bool Get(IMap map)
            => map.Markers.CharacterMarker.IsActive();
    }
}