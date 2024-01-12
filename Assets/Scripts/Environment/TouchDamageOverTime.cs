using System.Collections;
using Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(MeshCollider))]
    public class TouchDamageOverTime : MonoBehaviour
    {
        [SerializeField] private int touchDamage = 5;

        [CanBeNull] private GameCharacter touchingPlayer = null;
        private const float DamageIntervalSeconds = 0.3f;
        [CanBeNull] private Coroutine damageOverTime = null;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            touchingPlayer = other.GetComponent<GameCharacter>();
            damageOverTime = StartCoroutine(nameof(TakeDamageOverTime));
        }

        private IEnumerator TakeDamageOverTime()
        {
            while (true)
            {
                touchingPlayer!.TakeDamage(touchDamage);
                yield return new WaitForSeconds(DamageIntervalSeconds);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void Reset()
        {
            touchingPlayer = null;
            if (damageOverTime != null) StopCoroutine(damageOverTime);
            damageOverTime = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Reset();
        }
    }
}