namespace Remagures.Model.MeatSystem
{
    public interface ICookedMeatHeap
    {
        int Count { get; }
        
        void Add(int count);
        void Take(int count);
    }
}