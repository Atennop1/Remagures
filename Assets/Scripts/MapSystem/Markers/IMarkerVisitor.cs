namespace Remagures.MapSystem.Markers
{
    public interface IMarkerVisitor
    {
        public void Visit(IMarker marker);
    }
}