using System;
using Core;
using Environment;
using Player;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private int knockback;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (GetComponentInParent<GameCharacter>() &&
                other.gameObject.Equals(GetComponentInParent<GameCharacter>().gameObject)) return;
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                HitOnlyOnce(other, () =>
                {
                    //A character was hit, take away that precious hp
                    GameCharacter gameCharacter = other.GetComponent<GameCharacter>();
                    _playerController.onEnemyHitMelee.Invoke();
                    gameCharacter.TakeDamage(damage, DamageType.Melee);
                    KnockbackCharacter(gameCharacter);
                });
            }
            else if (other.TryGetComponent<Regrowable>(out var regrowable))
            {
                HitOnlyOnce(other, () =>
                {
                    _playerController.onBushCut.Invoke();
                    regrowable.CutDown();
                });
            }
        }

        private void HitOnlyOnce(Collider target, Action action)
        {
            if (_playerController.ObjectsHitInLastMeleeAttack.Contains(target)) return;

            action();

            _playerController.ObjectsHitInLastMeleeAttack.Add(target);
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