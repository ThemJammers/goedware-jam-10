using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class RandomMovement : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent;
        public float range; //radius of sphere

        public Transform centrePoint; //centre of the area the agent wants to move around in
        //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
        private bool _move = false;
        private Vector3 targetPosition = Vector3.zero;
        
        void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            StartMoving();
            GetTarget();
            InvokeRepeating(nameof(CheckIfArrived), 0, .5f);
        }

        public void StartMoving()
        {
            _move = true;
            agent.isStopped = false;
        }

        public void StopMoving()
        {
            _move = false;
            agent.isStopped = true;
            agent.enabled = false;
        }

        private void GetTarget()
        {
            bool foundRandomPoint = RandomPoint(centrePoint.position, range, out targetPosition);
            while (foundRandomPoint == false)
            {
                foundRandomPoint = RandomPoint(centrePoint.position, range, out targetPosition);
            }
            agent.SetDestination(targetPosition);
        }
    
        void CheckIfArrived()
        {
            if (!_move) return;
            if(agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                targetPosition = Vector3.zero;
                GetTarget();
                Debug.DrawRay(targetPosition, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            }

        }
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            UnityEngine.AI.NavMeshHit hit;
            bool foundPosition = false;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, .1f, UnityEngine.AI.NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            { 
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }
            
            result = Vector3.forward * 1;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(centrePoint.position, range);
        }
    }
}
