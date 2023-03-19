namespace Remagures.Model.MeatSystem
{
    public interface IMeatCooker
    {
        int RawMeatCount { get; }
        void CookMeat(int count);

        void RemoveCookedMeat();
        void AddRawMeat();
    }
}