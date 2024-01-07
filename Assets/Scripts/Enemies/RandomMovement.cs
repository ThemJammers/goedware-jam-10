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
        
        void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        public void StartMoving()
        {
            _move = true;
        }

        public void StopMoving()
        {
            _move = false;
        }
    
        void Update()
        {
            if (!_move) return;
            if(agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(point);
                }
            }

        }
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            { 
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(centrePoint.position, range);
        }
    }
}
