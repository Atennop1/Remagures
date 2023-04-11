namespace Remagures.Model.CutscenesSystem
{
    public interface ICutscene
    {
        bool IsStarted { get; }
        bool IsFinished { get; }

        void Start();
    }
}