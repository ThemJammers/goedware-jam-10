using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Weapons
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon playerWeapon;
        [SerializeField] private List<ProjectileData> projectiles;
        
        public void SelectWeapon(int weaponId)
        {
            playerWeapon.ChangeProjectile(projectiles[weaponId]);
        }

        public void AddProjectile(ProjectileData projectile) => projectiles.Add(projectile);
    }
}
