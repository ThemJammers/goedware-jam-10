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

        [CanBeNull] private GameCharacter _touchingPlayer = null;
        
        private const float DamageIntervalSeconds = 0.6f;
        [CanBeNull] private Coroutine _damageOverTime = null;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _touchingPlayer = other.GetComponent<GameCharacter>();
            _damageOverTime = StartCoroutine(nameof(TakeDamageOverTime));
        }

        private IEnumerator TakeDamageOverTime()
        {
            while (true)
            {
                if (_touchingPlayer == null) break;
                _touchingPlayer.TakeDamage(touchDamage);
                yield return new WaitForSeconds(DamageIntervalSeconds);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void Reset()
        {
            _touchingPlayer = null;
            if (_damageOverTime != null) StopCoroutine(_damageOverTime);
            _damageOverTime = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Reset();
        }
    }
}