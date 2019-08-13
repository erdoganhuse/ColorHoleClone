namespace Core.Signal.Stage
{
    public class StageLoadedSignal
    {
        public readonly int LevelIndex;
        public readonly int StageIndex;
        public readonly int AbsorbableObjectCount;
        
        public StageLoadedSignal(int levelIndex, int stageIndex, int absorbableObjectCount)
        {
            LevelIndex = levelIndex;
            StageIndex = stageIndex;
            AbsorbableObjectCount = absorbableObjectCount;
        }
    }
}