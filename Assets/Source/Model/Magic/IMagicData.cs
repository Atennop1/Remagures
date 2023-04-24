namespace Remagures.Model.Magic
{
    public interface IMagicData
    {
        int ManaCost { get; }
        int CooldownInMilliseconds { get; }
        int ApplyingTimeInMilliseconds { get; }
    }
}