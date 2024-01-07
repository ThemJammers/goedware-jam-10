using System;
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
        [SerializeField] protected ProjectileData projectileData;
        private Rigidbody _rigidbody;
        private bool active = true;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            StartCoroutine(DestroyWithDelay());
        }

        protected virtual void Start()
        {
            LaunchProjectile();
        }

        protected virtual void LaunchProjectile()
        {
            _rigidbody.AddForce(transform.forward * projectileData.speed, ForceMode.Impulse);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                if (active)
                {
                    //A character was hit, take away that precious hp
                    GameCharacter gameCharacter = other.GetComponent<GameCharacter>();
                    gameCharacter.TakeDamage(projectileData.damage);
                    KnockbackCharacter(gameCharacter);
                    Dispose();
                }
                active = false;
            }
            else
            {
                //Hit something else
                Dispose();
            }
        }

        protected virtual void KnockbackCharacter(GameCharacter gameCharacter)
        {
            Rigidbody rigidbody = gameCharacter.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * projectileData.knockbackPower;
            force.y = .25f;
            rigidbody.AddForce(force, ForceMode.Impulse);
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