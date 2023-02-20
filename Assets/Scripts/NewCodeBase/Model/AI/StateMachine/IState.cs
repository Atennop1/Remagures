namespace Remagures.Model.AI
{
    public interface IState
    {
        void Update();
        void OnEnter();
        void OnExit();
    }
}