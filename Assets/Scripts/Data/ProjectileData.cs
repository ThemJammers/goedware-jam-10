using UnityEngine;

namespace ThemJammers.Data
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/CreateProjectile", order = 1)]
    public class ProjectileData : ScriptableObject
    {
        public int damage;
        public float speed;
        public float lifetime = 5;
        public float shootingInterval = 1f;
        public GameObject prefab;
    }
}