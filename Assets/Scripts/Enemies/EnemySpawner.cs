using System;
using Unity.Mathematics;
using UnityEngine;

namespace Enemies
{
    /// <summary>
    /// Currently only for debugging purpose
    /// TODO: Implement proper spawning logic with randomized spawn points in a given area
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private GameObject enemyPrefab;
        private GameObject spawnedEnemy;
        private void Start()
        {
            SpawnEnemy();
        }

        // Update is called once per frame
        void Update()
        {
            if (spawnedEnemy == null)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            spawnedEnemy = Instantiate(enemyPrefab, spawnPosition.position, quaternion.identity);
        }
    }
}
