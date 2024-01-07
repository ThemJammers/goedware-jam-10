using Core;
using UnityEngine;
using Weapons;

namespace Enemies
{
    public class Enemy : GameCharacter
    {
        [SerializeField] private float detectionRadius = 5;
        [SerializeField] private float targetingSpeed = 1;
        protected Transform _playerTransform = null;
        protected Weapon _weapon;
        protected virtual void Awake()
        {
            _weapon = GetComponentInChildren<Weapon>();
            //Scan for the player once every second
            InvokeRepeating(nameof(ScanForPlayer), 0, 1);
        }

        protected virtual void Update()
        {
            if (_playerTransform != null)
            {
                TargetPlayer(); 
                ShootAtPlayer();
            }
        }

        protected virtual void TargetPlayer()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_playerTransform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, targetingSpeed * Time.deltaTime);
        }

        protected virtual void ShootAtPlayer()
        {
            if (_weapon.ShootingLocked == false)
            {
                _weapon.Shoot();
            }
        }
        
        protected virtual void ScanForPlayer()
        {
            RaycastHit hit;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, 1 << 7,
                QueryTriggerInteraction.Ignore); 
            //Check if the player is within detectionRadius
            if (hitColliders.Length > 0)
            {
                //Player detected within detectionRadius
                _playerTransform = hitColliders[0].transform;
                CancelInvoke(nameof(ScanForPlayer));
            }
        }

        protected virtual void OnDrawGizmos()
        {
            //Debug method to show if a player has been detected in scene view
            //Red = Detected; Blue = Scanning
            Gizmos.color = _playerTransform ? Color.red : Color.blue;
            Gizmos.DrawWireSphere(transform.position + transform.up, detectionRadius);
        }
    }
}
