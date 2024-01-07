using System.Collections;
using Data;
using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour, IShootable
    {
        [SerializeField] private Transform projectileSpawn;
        [SerializeField] private ProjectileData projectileData;

        protected bool shootingLocked = false;
        public bool ShootingLocked => shootingLocked;
        
        public void Shoot()
        {
            if (shootingLocked) return;
            //TODO: Implement object pool and let it handle projectiles
            Instantiate(projectileData.prefab, projectileSpawn.position, projectileSpawn.rotation);
            StartCoroutine(WaitForInterval());
        }

        protected virtual IEnumerator WaitForInterval()
        {
            shootingLocked = true;
            yield return new WaitForSecondsRealtime(projectileData.shootingInterval);
            shootingLocked = false;
        }
    }
}