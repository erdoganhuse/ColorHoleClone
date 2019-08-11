namespace Core.Signal.Level
{
    public class LevelLoadedSignal
    {
        public readonly int LevelId;
        
        public LevelLoadedSignal(int levelId)
        {
            LevelId = levelId;
        }
    }
}