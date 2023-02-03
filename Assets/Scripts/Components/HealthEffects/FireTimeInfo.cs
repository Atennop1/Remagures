namespace Remagures.Components
{
    public readonly struct FireInfo
    {
        public readonly float MinTime;
        public readonly float MaxTime;
        public readonly float Damage;

        public FireInfo(float minTime, float maxTime, float damage)
        {
            MinTime = minTime;
            MaxTime = maxTime;
            Damage = damage;
        }
    }
}