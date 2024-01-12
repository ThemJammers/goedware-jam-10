using Core;
using Interfaces;
using Loot;
using Sacrifices;
using UnityEngine;
using Weapons;

namespace Enemies
{
    public class Enemy : GameCharacter, ITurnable
    {
        //Radius that defines how far away the player can be detected
        [SerializeField] private float detectionRadius = 5;
        //Defines the speed in which the enemy can aim at the player
        [SerializeField] private float targetingSpeed = 1;
        [SerializeField] private Transform modelTransform;
        [SerializeField] protected EnemyTier tier = EnemyTier.Tier1;
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
                //Attack the player if it is in LoS
                if (IsPlayerInSight())
                {
                    Turn(Vector2.zero); 
                    ShootAtPlayer();
                }
            }
        }

        public void Turn(Vector2 input)
        {
            //Aim towards the player
            Quaternion targetRotation = Quaternion.LookRotation(_playerTransform.position - transform.position);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, targetingSpeed * Time.deltaTime);
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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, 1 << 7,
                QueryTriggerInteraction.Ignore); 
            //Check if the player is within detectionRadius
            if (hitColliders.Length > 0)
            {
                _playerTransform = hitColliders[0].transform;
                //Player detected within detectionRadius
                //Additionally check if player is in LoS
                if (IsPlayerInSight())
                {
                    CancelInvoke(nameof(ScanForPlayer));
                }
                else
                {
                    //Player is not in LoS -> Ignore it
                    _playerTransform = null;
                }
            }
        }

        private bool IsPlayerInSight()
        {
            //Check if player is actually in enemies Line of sight
            Vector3 directionToPlayer = _playerTransform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, 20, 1 << 7))
            {
                return hit.collider.CompareTag("Player");
            }
            return false;
        }

        public override void TakeDamage(int amount, DamageType damageType)
        {
            base.TakeDamage(amount, damageType);
            //If enemy does not yet have aggro on the player -> now it does
            if (_playerTransform == null)
            {
                _playerTransform = GameObject.FindWithTag("Player").transform;
                CancelInvoke(nameof(ScanForPlayer));
            }
        }

        public override void Die()
        {
            LootController.Instance.SpawnLoot(transform.position, tier);
            base.Die();
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
