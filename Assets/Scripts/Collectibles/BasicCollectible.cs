using Interfaces;
using Player;
using UnityEngine;

namespace Collectibles
{
    [RequireComponent(typeof(Collider))]
    public class BasicCollectible : MonoBehaviour, ICollectible
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController playerController = other.GetComponent<PlayerController>();
                Collect(playerController);
            }
        }

        public virtual void Collect(PlayerController playerController)
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
