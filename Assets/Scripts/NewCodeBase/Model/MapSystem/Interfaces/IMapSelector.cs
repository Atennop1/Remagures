namespace Remagures.Model.MapSystem
{
    public interface IMapSelector
    {
        IMap CurrentLocationMap { get; } //TODO make this map open on the first click on the open map button
        IMap SelectedMap { get; }
    }
}