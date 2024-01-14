using System;
using System.Collections;
using Core;
using Player;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public class HomingEnemy : Enemy
    {
        [SerializeField] private float speed;
        [SerializeField] private float movingRadius = 3;
        private Animator animator;
        private bool _homing = false;
        private bool _cooldown = false;
        private bool _moveForward = false;
        private Vector3 targetDirection = Vector3.zero;
        private Vector3 defaultPosition;
        public Vector3 direction;
        private Vector3 targetPosition;
        
        protected override void Awake()
        {
            base.Awake();
            defaultPosition = transform.position;
            animator = GetComponent<Animator>();
            MoveBackAndForth();
        }

        protected override void Update()
        {
            float dist = Vector3.Distance(transform.position, targetPosition);
            if (dist <= .1f)
            {
                MoveBackAndForth();
            }
            else
            {
                transform.Translate(Vector3.forward * Direction() * speed, Space.Self);
            }
        }

        private void MoveBackAndForth()
        {
            _moveForward = !_moveForward;
            targetPosition = ((defaultPosition) + (transform.forward * Direction()) * movingRadius);
            targetDirection = defaultPosition + (transform.forward * Direction());
            animator.SetBool("Moving", true);
            animator.SetBool("Backwards", Direction() < 0);
        }

        protected override void ScanForPlayer()
        {
            
        }

        public override void Turn(Vector2 input)
        {
            
        }

        private int Direction()
        {
            return _moveForward ? 1 : -1;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                GameCharacter character = other.gameObject.GetComponent<GameCharacter>();
                character.TakeDamage(50); //TODO: Implement damage parameter
            }
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 gizmoPosition = transform.position;
            Gizmos.DrawLine(defaultPosition != Vector3.zero ? defaultPosition : gizmoPosition, transform.position +  (transform.forward * movingRadius));
            Gizmos.DrawLine(defaultPosition != Vector3.zero ? defaultPosition : gizmoPosition, transform.position + (-transform.forward * movingRadius));

            Gizmos.DrawSphere(targetPosition, .25f);
        }
    }
}
