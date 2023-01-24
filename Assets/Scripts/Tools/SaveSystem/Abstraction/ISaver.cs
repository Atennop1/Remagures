namespace Remagures.SaveSystem.Abstraction
{
    public interface ISaver
    {
        public void Save();
        public void Load();
        public void NewGame();
    }
}
