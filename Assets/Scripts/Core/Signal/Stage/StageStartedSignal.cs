namespace Core.Signal.Stage
{
    public class StageStartedSignal
    {
        public readonly int StageIndex;

        public StageStartedSignal(int stageIndex)
        {
            StageIndex = stageIndex;
        }
    }
}