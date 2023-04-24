using Remagures.Tools;

namespace Remagures.Model.Flashing
{
    public readonly struct FlashingData : IFlashingData
    {
        public int FlashDurationInMilliseconds { get; }
        public int NumberOfFlashes { get; }

        public FlashingData(int flashDurationInMilliseconds, int numberOfFlashes)
        {
            FlashDurationInMilliseconds = flashDurationInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
            NumberOfFlashes = numberOfFlashes.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}