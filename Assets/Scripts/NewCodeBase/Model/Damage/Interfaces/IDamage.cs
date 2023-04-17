namespace Remagures.Model.Damage
{
    public interface IDamage
    {
        IDamageValue Value { get; }
        void ApplyTo(ITarget target);
    }
}