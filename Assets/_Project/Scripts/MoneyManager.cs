using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class MoneyManager : MonoBehaviour
    {
        private const int RewardForKill = 10;

        [SerializeField] private EnemyRegistry _enemyRegistry;
        public int Money { get; private set; }
        public event Action OnMoneyChanged;

        private void Start()
        {
            _enemyRegistry.OnEnemyDied += HandleEnemyDied;
        }

        private void OnDestroy()
        {
            _enemyRegistry.OnEnemyDied -= HandleEnemyDied;
        }

        private void Add(int amount)
        {
            Money += amount;
            OnMoneyChanged?.Invoke();
        }

        public bool TrySpend(int amount)
        {
            if (Money < amount)
            {
                return false;
            }

            Money -= amount;
            OnMoneyChanged?.Invoke();
            return true;
        }

        private void HandleEnemyDied()
        {
            Add(RewardForKill);
        }
    }
}