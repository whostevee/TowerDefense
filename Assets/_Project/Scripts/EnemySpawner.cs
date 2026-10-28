using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Tower _tower;
    [SerializeField] private Transform[] _spawnPoint;

    public void Spawn()
    {
        var enemy = Instantiate(_enemy, _spawnPoint[Random.Range(0, _spawnPoint.Length)].position, Quaternion.identity);
        
        GameManager.Instance.AddEnemy(enemy);
        
        enemy.SetTarget(_tower);
    }

}