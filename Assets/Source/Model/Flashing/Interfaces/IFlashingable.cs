namespace Remagures.Model.Flashing
{
    public interface IFlashingable
    {
        void Flash(FlashColorType flashColorType, FlashColorType afterFlashColorType, FlashingData data);
    }
}