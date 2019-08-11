namespace Core.Signal.Level
{
    public class LevelEndedSignal
    {
        public readonly int LevelId;
        public readonly bool IsSuccessful;

        public LevelEndedSignal(int levelId, bool isSuccessful)
        {
            LevelId = levelId;
            IsSuccessful = isSuccessful;
        }
    }
}