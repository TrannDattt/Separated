using UnityEngine;
using UnityEngine.UI;

namespace Separated.Helpers
{
    public static class GraphCalc
    {
        public static float EasyEaseIn(float a, float x) // y = x ^ a, a > 1, Speed up
        {
            return Mathf.Pow(x, a);
        }

        public static float EasyEaseOut(float a, float x) // y = x ^ a, 0 < a < 1, Slow down
        {
            return Mathf.Pow(x, a);
            // return Mathf.Pow(1 - x, a);
        }
    }
}
