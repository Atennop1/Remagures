namespace Remagures.Root
{
    public interface ISystemUpdate
    {
        void Add(params IUpdatable[] update);
        void Remove(IUpdatable update);
        void UpdateAll();
    }
}