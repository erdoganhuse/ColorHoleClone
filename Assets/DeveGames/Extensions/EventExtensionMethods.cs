using System;

namespace DeveGames.Extensions
{
    public static class EventExtensionMethods
    {
        public static T Chain<T>(this T source, Action<T> action)
        {
            action(source);
            return source;
        }

        #region Invoke Extensions

        public static void SafeInvoke(this Action source)
        {
            if(source != null) source.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> source, T value)
        {
            if (source != null) source.Invoke(value);
        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> source, T1 firstValue, T2 secondValue)
        {            
            if (source != null) source.Invoke(firstValue, secondValue);
        }

        public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> source, T1 firstValue, T2 secondValue, T3 thirdValue)
        {
            if (source != null) source.Invoke(firstValue, secondValue, thirdValue);
        }

        public static void SafeInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> source, T1 firstValue, T2 secondValue, T3 thirdValue, T4 fourthValue)
        {
            if (source != null) source.Invoke(firstValue, secondValue, thirdValue, fourthValue);
        }

        public static void SafeInvoke<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> source, T1 firstValue, T2 secondValue, T3 thirdValue, T4 fourthValue, T5 fifthValue)
        {
            if (source != null) source.Invoke(firstValue, secondValue, thirdValue, fourthValue, fifthValue);
        }

        public static void SafeInvoke<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> source, T1 firstValue, T2 secondValue, T3 thirdValue, T4 fourthValue, T5 fifthValue, T6 sixthValue)
        {
            if (source != null) source.Invoke(firstValue, secondValue, thirdValue, fourthValue, fifthValue, sixthValue);
        }

        #endregion
    }
}
