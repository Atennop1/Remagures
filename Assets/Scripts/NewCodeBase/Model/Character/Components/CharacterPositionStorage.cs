using SaveSystem;

namespace Remagures.Model.Character
{
    public class CharacterPositionStorage
    {
        private readonly ISaveStorage<CharacterPositionData> _characterPositionDataStorage;

        public CharacterPositionStorage(ISaveStorage<CharacterPositionData> characterPositionDataStorage)
            => _characterPositionDataStorage = characterPositionDataStorage;

        public void Save(CharacterPositionData data)
            => _characterPositionDataStorage.Save(data);

        public CharacterPositionData Load()
            => _characterPositionDataStorage.Load();
    }
}