namespace Core.Signal.Level
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