using System;

namespace _Project.Scripts.Back
{
    public class Health
    {
      public int CurrentHealth { get; private set; }
      public int MaxHealth { get; private set; }
      public bool IsDead
      {
          get { return CurrentHealth <= 0; }
      }

      public event Action OnHealthChanged;
      public event Action OnDeath;


      public void TakeDamage(int damage)
      {
          if (!IsDead)
          {
              CurrentHealth = Math.Max(0, CurrentHealth - damage);
              if (CurrentHealth <= 0)
              {
                  OnDeath?.Invoke();
              }
          }
      }
    }
}
