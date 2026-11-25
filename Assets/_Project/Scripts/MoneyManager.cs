using System;
using _Project.Scripts.Back;
using UnityEngine;

namespace _Project.Scripts
{
    public class MoneyManager : MonoBehaviour
    {
        private const int RewardForKill = 10;
        
        [SerializeField] private EnemyRegistry _enemyRegistry;
        
        public Wallet Wallet {get; private set;}
        public int Money => Wallet.Money;

        private void Awake()
        {
            Wallet = new Wallet();
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
            Wallet.SpendMoney(amount);
        }

        private void HandleEnemyDied()
        {
            Wallet.AddMoney(RewardForKill);
        }
    }
}