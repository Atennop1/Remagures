namespace Remagures.Model.UpgradeSystem
{
    public interface IUpgradesChain
    {
        bool CanAdvance { get; }
        void Advance();
    }
}