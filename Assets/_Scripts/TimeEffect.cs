using UnityEngine;
using System.Collections;

namespace Separated.Helpers
{
    public class TimeEffect : Singleton<TimeEffect>
    {
        public void SlowTime(float duration, float scale)
        {
            // StopAllCoroutines();
            StartCoroutine(SlowTimeCoroutine(duration, scale));

            IEnumerator SlowTimeCoroutine(float duration, float scale)
            {
                Time.timeScale = scale;
                Time.fixedDeltaTime = 0.02f / scale;

                yield return new WaitForSecondsRealtime(duration);

                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
            }
        }
    }
}
