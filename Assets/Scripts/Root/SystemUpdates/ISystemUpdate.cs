namespace Remagures.Root.SystemUpdates
{
    public interface ISystemUpdate
    {
        void Add(params IUpdatable[] update);
        void Remove(IUpdatable update);
        void UpdateAll();
    }
}