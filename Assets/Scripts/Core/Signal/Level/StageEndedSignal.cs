namespace Core.Signal.Level
{
    public class StageEndedSignal
    {
        public readonly int StageIndex;

        public StageEndedSignal(int stageIndex)
        {
            StageIndex = stageIndex;
        }
    }
}