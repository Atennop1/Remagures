namespace Remagures.Model.MapSystem
{
    public interface IMapSelector
    {
        IMap CurrentLocationMap { get; } 
        IMap SelectedMap { get; }
    }
}