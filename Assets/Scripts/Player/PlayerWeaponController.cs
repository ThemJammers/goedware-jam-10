using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon playerWeapon;
        [SerializeField] private List<ProjectileData> projectiles;

        public UnityEvent onProjectileAdded;

        public void SelectWeapon(int weaponIndex)
        {
            if (weaponIndex < 0 || weaponIndex > projectiles.Count - 1) return; // No such weapon, do nothing
            
            var projectile = projectiles[weaponIndex];
            playerWeapon.ChangeProjectile(projectile);
        }

        public void AddProjectile(ProjectileData projectile)
        {
            projectiles.Add(projectile);
            onProjectileAdded?.Invoke();
        }

        public Weapon Weapon => playerWeapon;
    }
}