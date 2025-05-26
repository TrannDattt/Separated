namespace Separated.CameraControl
{
    using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Transform _target;

        private Vector3 _offset;

        void OnEnable()
        {
            _offset = transform.position - _target.position;
        }

        private void LateUpdate()
        {
            if (_target == null) return;

            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}