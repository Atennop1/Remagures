namespace Remagures.Model.Damage
{
    public interface IDamage
    {
        int Value { get; }
        void ApplyTo(ITarget target);
    }
}