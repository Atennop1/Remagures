using Remagures.Tools;

namespace Remagures.Model.Flashing
{
    public readonly struct FlashingData
    {
        public readonly int FlashDurationInMilliseconds;
        public readonly int NumberOfFlashes;

        public FlashingData(int flashDurationInMilliseconds, int numberOfFlashes)
        {
            FlashDurationInMilliseconds = flashDurationInMilliseconds.ThrowExceptionIfLessOrEqualsZero();
            NumberOfFlashes = numberOfFlashes.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}