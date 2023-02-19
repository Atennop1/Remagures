namespace Remagures.Model.AI.StateMachine
{
    public interface IState
    {
        void Update();
        void OnEnter();
        void OnExit();
    }
}