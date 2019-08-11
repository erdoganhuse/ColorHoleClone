using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DeveGames.PopupSystem.Scripts
{
    public static class Utility
    {        
        public static Popup Get(this GameObject go)
        {
            return go.GetComponent<Popup>();
        }

        public static T Get<T>(this IEnumerable<Popup> popups) where T: Popup
        {
            return popups.First(item => item.GetType() == typeof(T)) as T;
        }

        public static bool Any<T>(this IEnumerable<Popup> popups) where T: Popup
        {
            return popups.Any(item => item.GetType() == typeof(T));
        }
    }
}

