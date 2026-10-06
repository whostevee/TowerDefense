using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private int _waveNumber = 1;
    [SerializeField] private int _enemiesPerWave = 5;
    [SerializeField] private float _spawnTime = 2f;
    [SerializeField] private int _waveDelay = 2;
    [SerializeField] private int _spawnedInWave;

    private float _lastTimeKill;
    private bool _isWaveFinished;
    public float SpawnTime => _spawnTime;
    public int EnemiesPerWave => _enemiesPerWave;

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (_spawnedInWave < _enemiesPerWave)
        {
            SpawnNextEnemy();
        }

        if (GameManager.Instance.Enemies.Count != 0)
        {
            _lastTimeKill += Time.deltaTime;
        }
        else
        {
            WaveFinished();
        }

        if (_isWaveFinished)
        {
            if (Time.time > _lastTimeKill + _waveDelay)
            {
                NextWave();
            }
            else
            {
                Debug.Log("Жду время");
            }
        }
    }

    private void StartWave()
    {
        _isWaveFinished = false;
        SpawnNextEnemy();
    }

    private void SpawnNextEnemy()
    {
        _enemySpawner.Spawn();
        _spawnedInWave++;
    }

    private void WaveFinished()
    {
        Debug.Log("Волна закончилась");
        _isWaveFinished = true;
    }

    private void NextWave()
    {
        _isWaveFinished = false;
        _spawnedInWave = 0;

        _enemiesPerWave += 10;
        _spawnTime -= 0.1f;

        Debug.Log("Новые условия спавна");
    }
}