using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyRegistry : MonoBehaviour
    {
        private List<Enemy> _enemies = new();

        public event Action OnEnemyDied;

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public void AddEnemy(Enemy enemy)
        {
            enemy.OnDied += HandleEnemyDied;
            _enemies.Add(enemy);
        }

        private void HandleEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= HandleEnemyDied;
            _enemies.Remove(enemy);
            OnEnemyDied?.Invoke();
        }
    }
}