namespace Core.Signal.Hole
{
    public class EnteredToHoleSignal
    {
        public readonly string ObjectTag;

        public EnteredToHoleSignal(string objectTag)
        {
            ObjectTag = objectTag;
        }
    }
}