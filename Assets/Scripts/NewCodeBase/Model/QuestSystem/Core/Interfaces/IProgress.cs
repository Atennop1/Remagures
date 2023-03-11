namespace Remagures.Model.QuestSystem
{
    public interface IProgress
    {
        int CurrentPoints { get; }
        int RequiredPoints { get; }
        void AddPoints(int amount);
    }
}