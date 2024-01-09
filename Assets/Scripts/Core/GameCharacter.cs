using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Core
{
    public abstract class GameCharacter : MonoBehaviour, IDamageable
    {
        public UnityEvent HealthChanged;
        
        public int Health { get; set; } = 100;
        
        public int Defense { get; set; } = 0;

        public float BaseDamage { get; set; } = 1f;
        
        public virtual void Die()
        {
            //TODO: Implement something actually cool
            Destroy(gameObject);
        }

        public virtual void TakeDamage(int amount)
        {
            //Apply damage to health
            //Based on Defense (where 100 would absorb all dmg and 0 take full dmg)
            float dmgAbsorption = (100 - Defense) / 100;
            int dmg = (int)(amount * dmgAbsorption);
            UpdateHealth(Health - dmg);
        }
        
        public virtual void TakeTrueDamage(int amount) => UpdateHealth(Health - amount);

        public virtual void Heal(int amount) => UpdateHealth(Health + amount);

        public virtual void RestoreHealth() => UpdateHealth(100);

        public virtual void UpdateHealth(int newHealth)
        {
            Health = Mathf.Clamp(newHealth, 0, 100);
            HealthChanged?.Invoke();;
            if (Health <= 0)
            {
                Die();
            }
        }
    }
}
