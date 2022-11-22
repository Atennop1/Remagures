namespace Remagures.Cutscenes.Actions
{
    public interface ICutsceneAction
    {
        void Start();
        bool IsStarted { get; }
        bool IsFinished { get; }
    }
}
