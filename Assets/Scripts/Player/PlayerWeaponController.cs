using System.Collections.Generic;
using Data;
using Extensions;
using UI;
using UnityEngine;
using UnityEngine.Events;
using Weapons;

namespace Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon playerWeapon;
        [SerializeField] private List<ProjectileData> projectiles;
        [SerializeField] private WeaponsUIController uiController;

        public UnityEvent onProjectileAdded;

        private void Start()
        {
            foreach (var (idx, projectileData) in projectiles.Enumerate())
            {
                uiController.UpdateSlotIdx(idx, projectileData);
            }

            uiController.ActivateSlotIdx(0);
        }

        public void SelectWeapon(int weaponIndex)
        {
            if (weaponIndex < 0 || weaponIndex > projectiles.Count - 1) return; // No such weapon, do nothing

            var projectile = projectiles[weaponIndex];

            if (projectile == playerWeapon.ActiveProjectile) return; // Already have it selected
            
            playerWeapon.ChangeProjectile(projectile);
            uiController.ActivateSlotIdx(weaponIndex);
        }

        public void AddProjectile(ProjectileData projectile)
        {
            projectiles.Add(projectile);
            onProjectileAdded?.Invoke();
            uiController.UpdateSlotIdx(projectiles.Count - 1, projectile);
        }

        public Weapon Weapon => playerWeapon;
    }
}