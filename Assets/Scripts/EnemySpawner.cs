using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Tower _tower;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private float _spawnTime = 2f;
    [SerializeField] private int _maxSpawnCount = 5;

    private float _nextSpawnTime = 0f;
    private int _spawnCount = 0;

    private void Update()
    {
        if (Time.time < _nextSpawnTime)
        {
            return;
        }

        if (_spawnCount >= _maxSpawnCount)
        {
            return;
        }
        
        Spawn();
        _nextSpawnTime = Time.time + _spawnTime;
    }

    private void Spawn()
    {
        var enemy = Instantiate(_enemy, _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity);
        enemy.name = $"Enemy - {_spawnCount}"; //удалить после создания нормального урона по врагам
        
        GameManager.Instance.AddEnemy(enemy);
        
        enemy.SetTarget(_tower);
        
        _spawnCount++;
    }
}