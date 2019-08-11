using DG.Tweening;

namespace DeveGames.Extensions
{
    public static class DOTweenExtensionMethods
    {
        public static void SafeComplete(this Tweener tweener, bool withCallbacks = true)
        {
            if (tweener != null && tweener.IsActive() && !tweener.IsComplete())
            {
                tweener.Complete(withCallbacks);
            }
        }

        public static void SafeComplete(this Sequence sequence, bool withCallbacks = true)
        {
            if (sequence != null && sequence.IsActive() && !sequence.IsComplete())
            {
                sequence.Complete(withCallbacks);
            }
        }

        public static void SafeKill(this Sequence sequence)
        {
            if (sequence != null && sequence.IsActive() && !sequence.IsComplete())
            {
                sequence.Kill();
            }
        }
    }
}