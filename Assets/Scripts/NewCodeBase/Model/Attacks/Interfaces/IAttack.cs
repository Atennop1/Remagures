namespace Remagures.Model.Attacks
{
    public interface IAttack
    {
        int Damage { get; }
        void ApplyTo(ITarget target);
    }
}