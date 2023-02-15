namespace Remagures.Model.Magic
{
    public interface IMagic
    {
        MagicData Data { get; }
        void Activate();
    }
}