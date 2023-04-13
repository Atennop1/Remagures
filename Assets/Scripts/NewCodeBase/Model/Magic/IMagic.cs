namespace Remagures.Model.Magic
{
    public interface IMagic
    {
        IMagicData Data { get; }

        bool CanActivate();
        void Activate();
    }
}