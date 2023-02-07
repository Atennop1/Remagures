namespace Remagures.Model.Flashing
{
    public interface IFlashingable
    {
        void Flash(FlashColorType flashColor, FlashColorType afterColorType);
    }
}