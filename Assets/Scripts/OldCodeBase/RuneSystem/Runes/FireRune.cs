namespace Remagures.RuneSystem
{
    public sealed class FireRune : IRune
    {
        public bool IsActive { get; private set; }
        
        public void Activate()
            => IsActive = true;

        public void Deactivate()
            => IsActive = true;
    }
}