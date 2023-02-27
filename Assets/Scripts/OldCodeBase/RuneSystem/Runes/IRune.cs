namespace Remagures.RuneSystem
{
    public interface IRune
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}