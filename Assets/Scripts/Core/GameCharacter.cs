using System;
using Interfaces;
using Player;
using Sacrifices;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Core
{
    public abstract class GameCharacter : MonoBehaviour, IDamageable
    {
        public UnityEvent HealthChanged;
        public UnityEvent onDamageTaken;
        
        public int Health { get; set; } = 100;
        
        public int Defense { get; set; } = 0;

        public float BaseDamage { get; set; } = 1f;
        private bool isPlayer = false;

        private void Awake()
        {
            isPlayer = GetComponent<PlayerController>();
        }

        public virtual void Die()
        {
            //TODO: Implement something actually cool
            Destroy(gameObject);
        }
        
        public virtual void TakeDamage(int amount, DamageType damageType = DamageType.Ranged)
        {
            amount = GetModifiedDamageValue(amount, damageType);
            //Apply damage to health
            //Based on Defense (where 100 would absorb all dmg and 0 take full dmg)
            int dmg = GetAbsorbedDamageValue(amount);
            UpdateHealth(Health - dmg);
        }

        private int GetAbsorbedDamageValue(int dmg)
        {
            float dmgAbsorption = (100 - Defense) / 100;
            Defense = Mathf.Clamp((int)(Defense - dmgAbsorption), 0, 100);
            return (int)(dmg * dmgAbsorption);
        }

        private int GetModifiedDamageValue(int dmg, DamageType damageType)
        {
            SacrificeController sacrificeController = SacrificeController.Instance;
            float modifier;
            switch (damageType)
            {
                case DamageType.Melee:
                    modifier =  isPlayer ? sacrificeController.EnemyMeleeModifier : sacrificeController.PlayerMeleeModifier;
                    return (int)(modifier * dmg);
                case DamageType.Ranged:
                    modifier =  isPlayer ? sacrificeController.EnemyProjectileModifier : sacrificeController.PlayerProjectileModifier;
                    return (int)(modifier * dmg);
                default:
                    throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
            }
        }

        public virtual void TakeTrueDamage(int amount) => UpdateHealth(Health - amount);

        public virtual void Heal(int amount) => UpdateHealth(Health + amount);

        public virtual void RestoreHealth() => UpdateHealth(100);

        private void UpdateHealth(int newHealth)
        {
            if (newHealth < Health)
            {
                onDamageTaken.Invoke();
            }
            
            Health = Mathf.Clamp(newHealth, 0, 100);
            HealthChanged?.Invoke();;
            if (Health <= 0)
            {
                Die();
            }
        }
    }
}
