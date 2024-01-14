using System;
using Traps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class RandomMovement : MonoBehaviour
    {
        private bool _move = false;
        private Vector3 movementDirection = Vector3.zero;
        [SerializeField] private float speed;
        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private Transform modelTransform;
        private Enemy enemy;
        
        void Start()
        {
            enemy = GetComponent<Enemy>();
            StartMoving();
        }

        public void StartMoving()
        {
            _move = true;
            GetRandomDirection();
        }

        public void StopMoving()
        {
            _move = false;
            movementDirection = Vector3.zero;
        }

        private void Update()
        {
            if (_move && movementDirection != Vector3.zero)
            {
                if (HitObstacle())
                {
                    GetRandomDirection();
                }
                transform.Translate(movementDirection * speed);
                Vector2 movement2D = new Vector2(movementDirection.x, movementDirection.z);
                modelTransform.LookAt(transform.position + movementDirection, Vector3.up);
            }
        }

        private void GetRandomDirection()
        {
            if (movementDirection != Vector3.zero)
            {
                Vector3 oppositeDirection = ((movementDirection + modelTransform.position) - modelTransform.position).normalized;
                movementDirection = oppositeDirection;
                if (!HitObstacle()) return;
            }

            movementDirection = RandomDirection();
        }

        private Vector3 RandomDirection()
        {
            return new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f));
        }

        private bool HitObstacle()
        {
            RaycastHit hit;
            Vector3 rayStart = modelTransform.position + (Vector3.up * 0.1f);
            //Check if any regular object blocks our path within .5m
            if (Physics.Raycast(rayStart, modelTransform.forward * .6f, out hit,
                    1, obstacleLayers))
            {
                if (hit.collider.gameObject != gameObject)
                {
                    return true;
                }    
            }
            //Check if we are running into a death rap, dont do that
            Collider[] suicideColliders = Physics.OverlapSphere(transform.position, .5f, 1 << 4);
            {
                if (suicideColliders.Length != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 rayStart = modelTransform.position + (Vector3.up * 0.1f);
            Gizmos.DrawLine(rayStart, rayStart + (modelTransform.forward * .6f));
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(rayStart, rayStart + movementDirection);
        }
    }
}
