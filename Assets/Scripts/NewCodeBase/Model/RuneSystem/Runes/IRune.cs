namespace Remagures.Model.RuneSystem
{
    public interface IRune
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}