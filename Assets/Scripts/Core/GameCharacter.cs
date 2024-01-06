using System;
using ThemJammers.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ThemJammers.Core
{
    public abstract class GameCharacter : MonoBehaviour, IDamageable
    {
        [FormerlySerializedAs("onHealthChanged")] public UnityEvent HealthChanged;
        public int Health { get; set; } = 100;
        public int Defense { get; set; } = 0;
        public virtual void Die()
        {
            throw new NotImplementedException();
        }

        public virtual void TakeDamage(int amount)
        {
            //Apply damage to health
            //Based on Defense (where 100 would absorb all dmg and 0 take full dmg)
            float dmgAbsorption = (100 - Defense) / 100;
            int dmg = (int)(amount * dmgAbsorption);
            UpdateHealth(Health - dmg);
            if (Health <= 0)
            {
                Die();
            }
        }

        public virtual void Heal(int amount)
        {
            UpdateHealth(Health + amount);
        }
        
        public virtual void RestoreHealth()
        {
            UpdateHealth(100);
        }

        public virtual void UpdateHealth(int newHealth)
        {
            Health = newHealth;
            HealthChanged?.Invoke();;
        }
    }
}
