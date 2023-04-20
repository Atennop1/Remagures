namespace Remagures.Root
{
    public interface ISystemUpdate
    {
        void UpdateAll();
        void Add(params IUpdatable[] update);
        void Remove(IUpdatable update);
    }
}