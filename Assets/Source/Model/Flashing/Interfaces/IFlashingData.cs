namespace Remagures.Model.Flashing
{
    public interface IFlashingData
    {
        int FlashDurationInMilliseconds { get; }
        int NumberOfFlashes { get; }
    }
}