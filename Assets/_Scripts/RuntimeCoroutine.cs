using System.Collections;
using UnityEngine;

namespace Separated.Helpers
{
    public class RuntimeCoroutine : Singleton<RuntimeCoroutine>
    {
        public Coroutine StartRuntimeCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);

        public void StopRuntimeCoroutine(IEnumerator coroutine) => StopCoroutine(coroutine);

        public void StopAllRuntimeCoroutines() => StopAllCoroutines();
    }
}
