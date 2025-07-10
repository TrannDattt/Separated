using Separated.Enums;
using Separated.GameManager;
using UnityEngine;
using UnityEngine.Events;

namespace Separated.Maps
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Gate : MonoBehaviour
    {
        public EMap Destination { get; private set; }
        private UnityEvent<EMap> _onEnteringGate;

        private void SetDestination(EMap destination)
        {
            Destination = destination;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            _onEnteringGate?.Invoke(Destination);
        }
    }
}