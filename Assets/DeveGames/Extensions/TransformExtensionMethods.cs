using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DeveGames.Extensions
{
    public static class TransformExtensionMethods
    {
        public static void AddLocalZ(this Transform transform, float z)
        {
            var newPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z);
            transform.localPosition = newPosition;
        }

        public static void ResetTransform(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }

        public static T GetComponentInChildrenWithTag<T>(this Transform transform, string tag) where T : Component
        {
            return transform.GetComponentsInChildrenWithTag<T>(tag).First();
        }
        
        public static T[] GetComponentsInChildrenWithTag<T>(this Transform transform, string tag) where T : Component
        {
            T[] allComponents = transform.GetComponentsInChildren<T>();
            List<T> matchedComponents = new List<T>();
            
            for (int i = 0; i < allComponents.Length; i++)
            {
                if (allComponents[i].transform.CompareTag(tag))
                {
                    matchedComponents.Add(allComponents[i]);
                }
            }

            return matchedComponents.ToArray();
        }
    }
}