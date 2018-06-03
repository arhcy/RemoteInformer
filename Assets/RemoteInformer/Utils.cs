using System;
using UnityEngine;

namespace artics
{
    public static class Utils
    {
        public const int RoundRange = 3;

        public static string PrintRoundedVector(String name, Vector3 vec)
        {
            return PrintName(name) + "X: " + RoundFloat(vec.x) + " Y: " + RoundFloat(vec.y) + " Z: " + RoundFloat(vec.z);
        }

        public static string PrintRoundedQuaternion(String name, Quaternion quat)
        {
            return PrintName(name) + "X: " + RoundFloat(quat.x) + " Y: " + RoundFloat(quat.y) + " Z: " + RoundFloat(quat.z) + " W: " + RoundFloat(quat.w);
        }

        public static string PrintName(String name)
        {
            return name.Length > 0 ? name + "\n" : "";
        }

        public static string RoundFloat(float value)
        {
            return Math.Round(value, RoundRange).ToString();
        }

        public static string PrintTouchInfo(Vector3[] positions, short[] phases)
        {
            string output = "Touches: ";

            if (positions == null)
                return output + "\n";

            for (int i = 0; i < positions.Length; i++)
                output += "[" + positions[i] + " phase:" + (short)phases[i] + "]  ";

            return output;
        }

        public static string PrintTouchArray(Touch[] array)
        {
            string output = "Touches: ";

            for (int i = 0; i < array.Length; i++)
                output += PrintTouchStructure(array[i]);

            return output;
        }

        public static string PrintTouchStructure(Touch value)
        {
            return "[" + value.position + " phase:" + value.phase + "]";
        }


    }
}
