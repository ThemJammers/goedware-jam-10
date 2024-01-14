using Interfaces;
using Player;
using Powerups;
using UnityEngine;

namespace Collectibles
{
    [RequireComponent(typeof(Collider))]
    public class Collectible : MonoBehaviour, ICollectible
    {
        [SerializeField] public PowerupEffect powerupEffect;
        [SerializeField] public AudioClip pickupSound;

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

            playerController.onItemCollected.Invoke(pickupSound);

            powerupEffect.Apply(playerController);
            powerupEffect.Apply(playerWeaponController);

            Dispose();
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}