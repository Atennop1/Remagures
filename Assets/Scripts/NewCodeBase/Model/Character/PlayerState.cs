namespace Remagures.Model.Character
{
    public enum PlayerState //TODO state machine for character
    {
        Idle,
        Walk,
        Stagger,
        Attack,
        Interact,
        Dead
    }
}