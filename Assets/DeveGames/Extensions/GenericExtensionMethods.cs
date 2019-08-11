using System;
using System.Collections.Generic;

namespace DeveGames.Extensions
{   
    public static class GenericExtensionMethods
    {
        public static T Next<T>(this T source) where T : struct
        {
            if (!typeof(T).IsEnum) 
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var values = (T[])Enum.GetValues(source.GetType());
            var index = Array.IndexOf<T>(values, source) + 1;
            
            return ( values.Length == index ) ? source : values[index];            
        }
        
        public static T Previous<T>(this T source) where T : struct
        {
            if (!typeof(T).IsEnum) 
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var values = (T[])Enum.GetValues(source.GetType());
            var index = Array.IndexOf<T>(values, source) - 1;
            
            return ( index < 0 ) ? source : values[index];            
        }

        public static T GetRandom<T>(this IList<T> source)
        {
            return source[UnityEngine.Random.Range(0, source.Count)];
        }
    }
}