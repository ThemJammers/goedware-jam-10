using Interfaces;
using JetBrains.Annotations;
using Player;
using Powerups;
using UnityEngine;
using UnityEngine.Localization.PropertyVariants.TrackedObjects;
using UnityEngine.Serialization;
using Weapons;

// ReSharper disable Unity.NoNullPropagation

namespace Collectibles
{
    [RequireComponent(typeof(Collider))]
    public class Collectible : MonoBehaviour, ICollectible
    {
        [SerializeField] public PowerupEffect powerupEffect;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect(other);
            }
        }

        public virtual void Collect(Collider player)
        {
            var playerController = player.GetComponent<PlayerController>();
            var playerWeaponController = player.GetComponent<PlayerWeaponController>();

            powerupEffect.Apply(playerController);
            powerupEffect.Apply(playerWeaponController);
            
            Dispose();
        }

        public void Collect(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}