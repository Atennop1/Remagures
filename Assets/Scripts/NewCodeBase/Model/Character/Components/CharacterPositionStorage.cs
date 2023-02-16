using Remagures.Tools;

namespace Remagures.Model.Character
{
    public class CharacterPositionStorage
    {
        private const string SAVE_FILE_NAME = "PlayerPositionData";
        private readonly IStorage _characterPositionDataStorage;

        public CharacterPositionStorage(IStorage characterPositionDataStorage)
            => _characterPositionDataStorage = characterPositionDataStorage;

        public void Save(CharacterPositionData data)
            => _characterPositionDataStorage.Save(data, SAVE_FILE_NAME);

        public CharacterPositionData Load()
            => _characterPositionDataStorage.Load<CharacterPositionData>(SAVE_FILE_NAME);
    }
}