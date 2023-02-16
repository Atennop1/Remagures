namespace Remagures.Tools
{
    public interface IStorage
    {
        T Load<T>(string path);
        void Save<T>(T saveObject, string path);
        
        bool Exist(string key);
        void DeleteSave(string path);
    }
}