using System.Collections;
using Core;
using Data;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileData projectileData;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.forward * projectileData.speed, ForceMode.Impulse);
            StartCoroutine(DestroyWithDelay());
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                //A character was hit, take away that precious hp
                GameCharacter gameCharacter = other.GetComponent<GameCharacter>();
                gameCharacter.TakeDamage(projectileData.damage);
                Dispose();
            }
            else
            {
                //Hit something else
                Dispose();
            }
        }

        protected virtual IEnumerator DestroyWithDelay()
        {
            //Dispose of projectiles after a fixed amount of time
            yield return new WaitForSecondsRealtime(projectileData.lifetime);
            Dispose();
        }

        protected virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}