using System;
using Core;
using Sacrifices;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private int knockback;
        private void OnTriggerEnter(Collider other)
        {
            if (GetComponentInParent<GameCharacter>() && other.gameObject.Equals(GetComponentInParent<GameCharacter>().gameObject)) return;
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                //A character was hit, take away that precious hp
                GameCharacter gameCharacter = other.GetComponent<GameCharacter>();
                gameCharacter.TakeDamage(damage, DamageType.Melee);
                KnockbackCharacter(gameCharacter);
            }
        }
        
        protected virtual void KnockbackCharacter(GameCharacter gameCharacter)
        {
            Rigidbody rigidbody = gameCharacter.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * knockback;
            //force.y = .25f;
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
