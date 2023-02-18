namespace Remagures.AI.StateMachine
{
    public interface IState
    {
        void Update();
        void OnEnter();
        void OnExit();
    }
}