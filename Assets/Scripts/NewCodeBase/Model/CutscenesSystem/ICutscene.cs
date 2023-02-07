namespace Remagures.Model.CutscenesSystem
{
    public interface ICutscene
    {
        void Start();
        bool IsStarted { get; }
        bool IsFinished { get; }
    }
}