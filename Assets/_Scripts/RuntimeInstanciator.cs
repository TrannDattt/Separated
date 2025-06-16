using Unity.Mathematics;
using UnityEngine;

namespace Separated.Helpers
{
    public class RuntimeInstanciator<T> where T : Object
    {
        public static T RuntimeInstantiate(T prefab, Vector3 pos = default, Quaternion rot = default, Transform parent = null)
        {
            var newObj = Object.Instantiate(prefab, pos, rot, parent);
            return newObj;
        }
    }
}
