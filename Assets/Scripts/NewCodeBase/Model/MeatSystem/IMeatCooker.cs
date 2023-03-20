namespace Remagures.Model.MeatSystem
{
    public interface IMeatCooker
    {
        int RawMeatCount { get; }
        
        void AddRawMeat();
        void CookMeat(int count);
    }
}