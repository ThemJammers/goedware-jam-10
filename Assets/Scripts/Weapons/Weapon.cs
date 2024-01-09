using System.Collections;
using Data;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour, IShootable
    {
        [SerializeField] protected Transform projectileSpawn;
        [SerializeField] protected ProjectileData projectileData;

        protected bool shootingLocked = false;
        protected Projectile currentProjectile;
        public bool ShootingLocked => shootingLocked;
        
        public virtual void Shoot()
        {
            if (shootingLocked) return;
            //TODO: Implement object pool and let it handle projectiles
            InstantiateProjectile();
            StartCoroutine(WaitForInterval());
        }

        protected virtual void InstantiateProjectile()
        {
            GameObject projectile = Instantiate(projectileData.prefab, projectileSpawn.position, projectileSpawn.rotation, projectileSpawn);
            currentProjectile = projectile.GetComponent<Projectile>();
        }

        public virtual void ChangeProjectile(ProjectileData newProjectile)
        {
            projectileData = newProjectile;
        }

        protected virtual IEnumerator WaitForInterval()
        {
            shootingLocked = true;
            yield return new WaitForSecondsRealtime(projectileData.shootingInterval);
            shootingLocked = false;
        }
    }
}