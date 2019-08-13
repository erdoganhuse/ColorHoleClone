namespace Core.Signal.Stage
{
    public class StageEndedSignal
    {
        public readonly int StageIndex;
        public readonly bool IsSuccessful;
        
        public StageEndedSignal(int stageIndex, bool isSuccessful)
        {
            StageIndex = stageIndex;
            IsSuccessful = isSuccessful;
        }
    }
}