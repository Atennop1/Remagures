namespace Remagures.Model.MeatSystem
{
    public interface IMeatCooker
    {
        int RawMeatCount { get; }
        
        void AddRawMeat(int count);
        void CookMeat(int count);
    }
}