using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class BallistaWeapon : Weapon
    {
        [SerializeField] private Animator animator;

        protected virtual void Awake()
        {
            InstantiateProjectile();
        }

        public override void Shoot()
        {
            if (shootingLocked) return;
            StartCoroutine(WaitForInterval());
            if (currentProjectile == null) InstantiateProjectile();
            animator.SetTrigger("Shoot");
            currentProjectile.LaunchProjectile();
        }

        protected override IEnumerator WaitForInterval()
        {
            shootingLocked = true;
            yield return new WaitForSecondsRealtime(projectileData.shootingInterval * 0.5f);
            InstantiateProjectile();
            yield return new WaitForSecondsRealtime(projectileData.shootingInterval * 0.5f);
            shootingLocked = false;
        }
    }
}
