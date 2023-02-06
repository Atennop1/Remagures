namespace Remagures.Model.Cutscenes
{
    public interface ICutscene
    {
        void Start();
        bool IsStarted { get; }
        bool IsFinished { get; }
    }
}