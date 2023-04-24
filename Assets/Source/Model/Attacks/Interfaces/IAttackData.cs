namespace Remagures.Model.Attacks
{
    public interface IAttackData
    {
        int UsingCooldownInMilliseconds { get; }
        int AttackingTimeInMilliseconds { get; }
    }
}