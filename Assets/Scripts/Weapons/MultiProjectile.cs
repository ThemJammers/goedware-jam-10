using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class MultiProjectile : Projectile
    {
        [SerializeField] private float spreadAngle = 90;
        [SerializeField] int projectileCount = 3;
        [SerializeField] private GameObject shellPrefab;
        
        public override void LaunchProjectile()
        {
            Quaternion defaultRotation = transform.rotation;
            //Fire first projectile from the left, e.g. 90° spread angle, start at -45°
            float startAngle = -(spreadAngle * 0.5f);
            //How many rotation steps do we need for all projectiles?
            float angleStep = spreadAngle / projectileCount;
            //Get into position for first projectile
            transform.Rotate(Vector3.up, startAngle);
            for (int i = 0; i < projectileCount; i++)
            {
                //Shot projectile
                Instantiate(shellPrefab, transform.position + (transform.forward * .5f), transform.rotation);
                //Get into position for next projectile
                transform.Rotate(Vector3.up, angleStep);
            }
            //Revert to default rotation
            transform.rotation = defaultRotation;
        }
    }
}
