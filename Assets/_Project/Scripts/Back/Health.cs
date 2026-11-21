using System;

namespace _Project.Scripts.Back
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; set; }

        public event Action<int, int> OnHealthChanged;
        public event Action OnDied;

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Math.Max(0, CurrentHealth - damage);
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
            
            if (CurrentHealth <= 0)
            {
                OnDied?.Invoke();
            }
        }
    }
}