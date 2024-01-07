using Core;
using UnityEngine;

namespace Traps
{
    [RequireComponent(typeof(MeshCollider))]
    public class SpikeTrap : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            {
                GameCharacter gameCharacter = other.gameObject.GetComponent<GameCharacter>();
                gameCharacter.TakeDamage(100);
            }
        }
    }
}
