namespace Remagures.Root
{
    public interface ILateSystemUpdate
    {
        void UpdateAll();
        void Add(params ILateUpdatable[] update);
        void Remove(ILateUpdatable update);
    }
}