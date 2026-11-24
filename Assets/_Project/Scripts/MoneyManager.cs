using System;
using _Project.Scripts.Back;
using UnityEngine;

namespace _Project.Scripts
{
    public class MoneyManager : MonoBehaviour
    {
        private const int RewardForKill = 10;
        
        private Wallet _wallet;
        [SerializeField] private EnemyRegistry _enemyRegistry;
        public int Money => _wallet.Money;
        public Wallet Wallet => _wallet;

        private void Awake()
        {
            _wallet = new Wallet();
        }
        private void Start()
        {
            _enemyRegistry.OnEnemyDied += HandleEnemyDied;
        }

        private void OnDestroy()
        {
            _enemyRegistry.OnEnemyDied -= HandleEnemyDied;
        }

        public void SpendMoney(int amount)
        {
            _wallet.SpendMoney(amount);
        }

        private void HandleEnemyDied()
        {
            _wallet.AddMoney(RewardForKill);
        }
    }
}