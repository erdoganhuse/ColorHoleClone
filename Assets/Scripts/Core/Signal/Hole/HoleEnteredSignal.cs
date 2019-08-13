namespace Core.Signal.Hole
{
    public class HoleEnteredSignal
    {
        public readonly string ObjectTag;

        public HoleEnteredSignal(string objectTag)
        {
            ObjectTag = objectTag;
        }
    }
}