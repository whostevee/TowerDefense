using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Tower _tower;
    [SerializeField] private Transform[] _spawnPoint;

    private float _spawnTime;
    private int _spawnCount;
    private float _nextSpawnTime;
    private int _currentCount;
    

    private void Start()
    {
        _spawnTime = _waveManager.SpawnTime;
        _spawnCount = _waveManager.EnemiesPerWave;
    }

    private void Update()
    {
        if (Time.time < _nextSpawnTime)
        {
            return;
        }
        
        if (_currentCount >= _spawnCount)
        {
            return;
        }
        
        _nextSpawnTime = Time.time + _spawnTime;
    }

    public void Spawn()
    {
        var enemy = Instantiate(_enemy, _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity);
        
        GameManager.Instance.AddEnemy(enemy);
        
        enemy.SetTarget(_tower);
        
        _currentCount++;
    }

}