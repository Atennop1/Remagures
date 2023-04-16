namespace Remagures.Model.MapSystem
{
    public interface IMapTransition
    {
        IMap MapToTransit { get; }
        void Transit();
    }
}