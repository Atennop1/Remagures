namespace Remagures.Model.CutscenesSystem
{
    public interface ICutsceneAction
    {
        void Start();
        void Finish();
        
        bool IsStarted { get; }
        bool IsFinished { get; }
    }
}
