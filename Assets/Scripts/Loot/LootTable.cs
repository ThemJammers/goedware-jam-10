using System.Collections.Generic;
using Loot;
using UnityEngine;

namespace Loot
{    
    [System.Serializable]
    [CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/Loot Table", order = 1)]
    public class LootTable : ScriptableObject
    {
        public List<LootItem> Items;
    }
}