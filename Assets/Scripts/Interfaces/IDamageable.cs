namespace ThemJammers.Interfaces
{
    public interface IDamageable
    {
        public int Health { get; set; }
        public int Defense { get; set; }
        public void Die();
        public void TakeDamage(int amount);
        public void Heal(int amount);
        public void RestoreHealth();
    }
}