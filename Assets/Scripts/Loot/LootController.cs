using System;
using System.Collections.Generic;
using Enemies;
using Patterns;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Loot
{
    public class LootController : Singleton<LootController>
    {
        [SerializeField] private LootTable tier1Loot;
        [SerializeField] private LootTable tier2Loot;
        [SerializeField] private LootTable tier3Loot;
        [SerializeField] private LootTable bossLoot;
        [SerializeField] private int dropPercentage = 30;
        public void SpawnLoot(Vector3 position, EnemyTier enemyTier)
        {
            if (DropLoot() == false) return;
            LootQuality quality = DetermineLootQuality();
            LootTable lootTable;
            switch (enemyTier)
            {
                case EnemyTier.Tier1:
                    lootTable = tier1Loot;
                    break;
                case EnemyTier.Tier2:
                    lootTable = tier2Loot;
                    break;
                case EnemyTier.Tier3:
                    lootTable = tier3Loot;
                    break;
                case EnemyTier.Boss:
                    lootTable = bossLoot;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyTier), enemyTier, null);
            }
            LootItem itemToDrop = GetLootItem(lootTable, quality);
            if (itemToDrop == null) return;
            Instantiate(itemToDrop.prefab, position, quaternion.identity);
        }

        private bool DropLoot()
        {
            int random = Random.Range(0, 100);
            return random <= dropPercentage;
        }

        private LootItem GetLootItem(LootTable lootTable, LootQuality quality)
        {
            List<LootItem> itemsWithQuality = lootTable.Items.FindAll(x => x.quality == quality);
            if (itemsWithQuality.Count == 0 || itemsWithQuality == null)
            {
                Debug.Log($"No Items with Quality {quality} are available for the given lootTable :(");
                return null;
            }
            int listLength = itemsWithQuality.Count;
            int random = Random.Range(0, listLength);
            return itemsWithQuality[random];
        }
        
        private LootQuality DetermineLootQuality()
        {
            int random = Random.Range(0, 100);
            if (random > 95)
            {
                return LootQuality.Epic;
            }
            if (random > 80)
            {
                return LootQuality.Common;
            }
            return LootQuality.Common;
        }
    }
}
