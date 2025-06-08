using UnityEngine;
using System.Collections;

namespace Separated.Helpers
{
    public class RuntimeCoroutine : Singleton<RuntimeCoroutine>
    {
        public void StartRuntimeCoroutine(IEnumerator coroutine)
        {
            if (coroutine != null)
            {
                StartCoroutine(coroutine);
            }
        }

        public void StopRuntimeCoroutine(IEnumerator coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        public void StopAllRuntimeCoroutines()
        {
            base.StopAllCoroutines();
        }
    }
}
