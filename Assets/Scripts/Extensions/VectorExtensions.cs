using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 WithX(this Vector3 vector, float newValue)
        {
            return new Vector3(newValue, vector.y, vector.z);
        }
        
        public static Vector3 WithY(this Vector3 vector, float newValue)
        {
            return new Vector3(vector.x, newValue, vector.z);
        }
        
        public static Vector3 WithZ(this Vector3 vector, float newValue)
        {
            return new Vector3(vector.x, vector.y, newValue);
        }
    }
}