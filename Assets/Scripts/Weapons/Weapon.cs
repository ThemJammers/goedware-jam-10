using System;
using System.Collections;
using Data;
using Interfaces;
using Player;
using Sacrifices;
using UnityEngine;
using UnityEngine.Events;

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

        private bool isPlayerWeapon;
        private SacrificeController _sacrificeController;

        public ProjectileData ActiveProjectile => projectileData;
        
        private void Start()
        {
            if (projectileData != null) onProjectileChanged.Invoke(projectileData);
            isPlayerWeapon = GetComponentInParent<PlayerController>();
            _sacrificeController = SacrificeController.Instance;
        }

        private void OnEnable()
        {
            shootingLocked = false;
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
            StartCoroutine(currentProjectile.GetComponent<Projectile>().Init());
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
            float factoredShootingInterval = (isPlayerWeapon ? (1 / _sacrificeController.PlayerCadenceModifier) : 1) * projectileData.shootingInterval;
            yield return new WaitForSecondsRealtime(factoredShootingInterval);
            shootingLocked = false;
        }
    }
}