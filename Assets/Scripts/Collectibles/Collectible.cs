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

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var playerController = other.GetComponent<PlayerController>();
                Collect(playerController);
            }
        }

        public virtual void Collect(PlayerController playerController)
        {
            powerupEffect.Apply(playerController);
            Dispose();
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}