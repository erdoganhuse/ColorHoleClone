using System.Collections.Generic;
using UnityEngine;

namespace DeveGames.Extensions
{
    public static class PhysicExtensionMethods
    {
        public static Vector3[] GetBoxColliderVertexPoints(BoxCollider boxCollider)
        {
            var vertices = new Vector3[8];
            var size = boxCollider.size;
            vertices[0] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-size.x, -size.y, -size.z) * 0.5f);
            vertices[1] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(size.x, -size.y, -size.z) * 0.5f);
            vertices[2] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(size.x, -size.y, size.z) * 0.5f);
            vertices[3] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-size.x, -size.y, size.z) * 0.5f);
            vertices[4] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-size.x, size.y, -size.z) * 0.5f);
            vertices[5] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(size.x, size.y, -size.z) * 0.5f);
            vertices[6] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(size.x, size.y, size.z) * 0.5f);
            vertices[7] = boxCollider.transform.TransformPoint(boxCollider.center + new Vector3(-size.x, size.y, size.z) * 0.5f);
            return vertices;
        }
        
        public static float GetBoundsContainedPercentage_Old(this Collider region, Collider enteredObject)
        {
            var total = 1f;
            
            for ( var i = 0; i < 3; i++ )
            {
                var dist = enteredObject.bounds.min[i] > region.bounds.center[i]
                    ? enteredObject.bounds.max[i] - region.bounds.max[i]
                    : region.bounds.min[i] - enteredObject.bounds.min[i];
 
                total *= Mathf.Clamp01(1f - dist / enteredObject.bounds.size[i]);
            }
 
            return total;
        }
    }
}