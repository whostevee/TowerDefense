using UnityEngine;

namespace _Project.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Tower _tower;
        [SerializeField] private Transform[] _spawnPoint;
        [SerializeField] private EnemyRegistry _enemyRegistry;

        public void Spawn()
        {
            var enemy = Instantiate(_enemy, _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity);
        
            _enemyRegistry.AddEnemy(enemy);
        
            enemy.SetTarget(_tower);
        }

    }
}