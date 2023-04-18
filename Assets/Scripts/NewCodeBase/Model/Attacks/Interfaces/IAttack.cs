namespace Remagures.Model.Attacks
{
    public interface IAttack
    {
        IAttackData Data { get; }
        void Use();
    }
}