using System;
using UnityEngine;

namespace artics
{
    public static class Utils
    {
        public static string PrintRoundedVector(Vector3 vec)
        {
            return "X: " + RoundFloat(vec.x) + " Y: " + RoundFloat(vec.y) + " Z: " + RoundFloat(vec.z);
        }

        public static string PrintRoundedQuaternion(Quaternion quat)
        {
            return "X: " + RoundFloat(quat.x) + " Y: " + RoundFloat(quat.y) + " Z: " + RoundFloat(quat.z) + " W: " + RoundFloat(quat.w);
        }

        public static string RoundFloat(float value)
        {
            return Math.Round(value, 3).ToString();
        }
    }
}
