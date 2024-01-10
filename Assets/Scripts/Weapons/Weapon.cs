using System;
using System.Collections;
using Data;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Weapons
{
    public class Weapon : MonoBehaviour, IShootable
    {
        [SerializeField] protected Transform projectileSpawn;
        [SerializeField] protected ProjectileData projectileData;

        protected bool shootingLocked = false;
        protected Projectile currentProjectile;
        public bool ShootingLocked => shootingLocked;

        public UnityEvent<ProjectileData> onProjectileChanged;
        public UnityEvent onShoot;

        private void Start()
        {
            if (projectileData != null) onProjectileChanged.Invoke(projectileData);
        }

        public virtual void Shoot()
        {
            if (shootingLocked) return;
            //TODO: Implement object pool and let it handle projectiles
            InstantiateProjectile();
            onShoot.Invoke();
            StartCoroutine(WaitForInterval());
        }

        protected virtual void InstantiateProjectile()
        {
            GameObject projectile = Instantiate(projectileData.prefab, projectileSpawn.position,
                projectileSpawn.rotation, projectileSpawn);
            currentProjectile = projectile.GetComponent<Projectile>();
        }

        public virtual void ChangeProjectile(ProjectileData newProjectile)
        {
            if (projectileData == newProjectile) return;
            
            projectileData = newProjectile;
            onProjectileChanged?.Invoke(newProjectile);
        }

        protected virtual IEnumerator WaitForInterval()
        {
            shootingLocked = true;
            yield return new WaitForSecondsRealtime(projectileData.shootingInterval);
            shootingLocked = false;
        }
    }
}