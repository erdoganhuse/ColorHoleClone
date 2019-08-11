namespace Core.Signal.Level
{
    public class LevelStartedSignal
    {
        public readonly int LevelId;

        public LevelStartedSignal(int levelId)
        {
            LevelId = levelId;
        }
    }
}