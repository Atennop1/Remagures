namespace Remagures.Factories
{
    public interface IFactory<T>
    {
        T Create();
    }
}