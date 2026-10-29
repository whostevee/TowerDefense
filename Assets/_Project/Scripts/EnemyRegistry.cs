using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyRegistry : MonoBehaviour
    {
        private const int RewardForKill = 10;

        [SerializeField] private MoneyManager _moneyManager;
        [SerializeField] private StatsManager _statsManager;

        private List<Enemy> _enemies;

        public IReadOnlyList<Enemy> Enemies => _enemies;

        private void Awake()
        {
            _enemies = new List<Enemy>();
        }

        private void Start()
        {
            Enemy.OnEnemyDied += HandleEnemyDied;
        }

        public void AddEnemy(Enemy e)
        {
            _enemies.Add(e);
        }

        public void RemoveEnemy(Enemy e)
        {
            _enemies.Remove(e);
        }

        private void HandleEnemyDied(Enemy e)
        {
            _moneyManager.Add(RewardForKill);

            _statsManager.UpdateDiedEnemies();
            RemoveEnemy(e);
        }
    }
}