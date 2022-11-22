namespace Remagures.Cutscenes
{
    public interface ICutscene
    {
        void Start();
        bool IsStarted { get; }
        bool IsFinished { get; }
    }
}