using Data;
using UnityEngine;
using Weapons;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Powerups/ProjectilePickup")]
    public class ProjectilePickup : PowerupEffect
    {
        [SerializeField] public ProjectileData projectile;

        public override void Apply(PlayerWeaponController weaponController)
        {
            weaponController.AddProjectile(projectile);
        }
    }
}
