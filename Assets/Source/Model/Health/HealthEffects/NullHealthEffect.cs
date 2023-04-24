namespace Remagures.Model.Health
{
    public sealed class NullHealthEffect : IHealthEffect
    {
        public void Activate() { }
        public void Stop()  { }
    }
}