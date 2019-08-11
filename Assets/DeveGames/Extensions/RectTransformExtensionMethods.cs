using System.Collections.Generic;
using UnityEngine;

namespace DeveGames.Extensions
{
    public static class RectTransformExtensionMethods
    {
        public static void SetAnchor(this RectTransform rectTransform, TextAnchor anchor)
        {
            rectTransform.anchorMin = AnchorPresets[anchor];
            rectTransform.anchorMax = AnchorPresets[anchor];
        }

        public static void SetPivot(this RectTransform rectTransform, TextAnchor pivot)
        {
            rectTransform.pivot = AnchorPresets[pivot];            
        }

        private static readonly Dictionary<TextAnchor, Vector2> AnchorPresets = new Dictionary<TextAnchor, Vector2>()
        {
            {TextAnchor.UpperLeft, new Vector2(0f, 1f)},
            {TextAnchor.UpperCenter, new Vector2(0.5f, 1f)},
            {TextAnchor.UpperRight, new Vector2(1f, 1f)},
            {TextAnchor.MiddleLeft, new Vector2(0f, 0.5f)},
            {TextAnchor.MiddleCenter, new Vector2(0.5f, 0.5f)},
            {TextAnchor.MiddleRight, new Vector2(1f, 0.5f)},
            {TextAnchor.LowerLeft, new Vector2(0f, 0f)},
            {TextAnchor.LowerCenter, new Vector2(0.5f, 0f)},
            {TextAnchor.LowerRight, new Vector2(1f, 0f)}
        };       
    }
}