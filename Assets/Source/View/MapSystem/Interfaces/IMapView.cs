using Remagures.Model.MapSystem;

namespace Remagures.View.MapSystem
{
    public interface IMapView
    {
        void Display(IMap map);
        void DisplayFailure();
    }
}