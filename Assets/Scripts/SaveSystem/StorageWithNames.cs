using System.IO;
using Remagures.SaveSystem.SwampAttack.Runtime.Tools.SaveSystem;

namespace Remagures.SaveSystem
{
    public class StorageWithNames<TUser, TObject>
    {
        private readonly string _path;
        private readonly BinaryStorage _storage;
        
        public StorageWithNames()
        {
            _storage = new BinaryStorage();
            _path = Path.Combine(typeof(TUser).Name, typeof(TObject).Name);
        }

        public void Save<T>(T item) => _storage.Save(item, _path);
        public T Load<T>() => _storage.Load<T>(_path);
        public bool Exist() => _storage.Exist(_path);
        public void DeleteSave() => _storage.DeleteSave(_path);
    }
}