using UnityEngine;
using System.Collections;

namespace Separated.Helpers
{
    public class CameraEffect : Singleton<CameraEffect>
    {
        private Camera _camera;

        public void ShakeCamera(float duration, float magnitude)
        {
            StopAllCoroutines();
            StartCoroutine(ShakeCameraCoroutine(duration, magnitude));

            IEnumerator ShakeCameraCoroutine(float duration, float magnitude)
            {
                Vector3 originalPosition = _camera.transform.localPosition;

                float elapsed = 0f;
                while (elapsed < duration)
                {
                    float x = Random.Range(-1f, 1f) * magnitude;
                    float y = Random.Range(-1f, 1f) * magnitude;

                    _camera.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

                    elapsed += Time.deltaTime;
                    yield return null;
                }

                _camera.transform.localPosition = originalPosition;
            }
        }

        void OnEnable()
        {
            _camera = Camera.main;
            if (_camera == null)
            {
                Debug.LogError("CameraEffect: No main camera found.");
            }
        }
    }
}
