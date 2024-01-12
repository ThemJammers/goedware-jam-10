using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Environment
{
    [RequireComponent(typeof(SphereCollider))]
    public sealed class GrowVicinityCollider : MonoBehaviour
    {
        public UnityEvent<GameCharacter> onPlayerEnter;
        public UnityEvent onPlayerExit;

        private SphereCollider _growDistanceCollider;

        public float PlayerVicinityRadius
        {
            set => GetComponent<SphereCollider>().radius = value;
        }

        private void Awake()
        {
            _growDistanceCollider = GetComponent<SphereCollider>();
            _growDistanceCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onPlayerEnter?.Invoke(other.GetComponent<GameCharacter>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            onPlayerExit?.Invoke();
        }
    }
}