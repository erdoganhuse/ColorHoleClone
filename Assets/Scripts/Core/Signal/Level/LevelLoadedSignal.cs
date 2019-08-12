namespace Core.Signal.Level
{
    public class LevelLoadedSignal
    {
        public readonly int LevelId;
        public readonly int StageCount;
        
        public LevelLoadedSignal(int levelId, int stageCount)
        {
            LevelId = levelId;
            StageCount = stageCount;
        }
    }
}