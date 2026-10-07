using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private enum WaveState
    {
        Spawning,
        WaitingForClear,
        Pause,
        Win
    }

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private int _maxWaves = 5;
    [SerializeField] private int _enemiesPerWave = 5;
    [SerializeField] private float _spawnTime = 0.5f;
    [SerializeField] private int _waveDelay = 10;
    [SerializeField] private int _spawnedInWave;

    private WaveState _state;

    private float _nextSpawnTime;
    private float _waveEndTime;
    private int _waveNumber = 1;

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        switch (_state)
        {
            case WaveState.Spawning:
                UpdateSpawning();
                break;
            case WaveState.WaitingForClear:
                UpdateWaiting();
                break;
            case WaveState.Pause:
                UpdatePause();
                break;
            case WaveState.Win:
                break;
        }
    }

    private void UpdateSpawning()
    {
        if (Time.time < _nextSpawnTime)
        {
            return;
        }

        if (_spawnedInWave >= _enemiesPerWave)
        {
            _state = WaveState.WaitingForClear;
            return;
        }

        SpawnNextEnemy();
        _nextSpawnTime = Time.time + _spawnTime;
    }

    private void UpdateWaiting()
    {
        if (GameManager.Instance.Enemies.Count == 0)
        {
            _waveEndTime = Time.time;
            
            Debug.Log("Жду время");
            _state = WaveState.Pause;
        }
    }

    private void UpdatePause()
    {
        if (_waveNumber >= _maxWaves)
        {
            Debug.Log("You Win");
            _state = WaveState.Win;
            return;
        }

        if (Time.time > _waveEndTime + _waveDelay)
        {
            _waveNumber++;

            NextWave();
            StartWave();
        }
    }

    private void StartWave()
    {
        Debug.Log("Wave #" + _waveNumber);
        _spawnedInWave = 0;
        _nextSpawnTime = Time.time + _spawnTime;
        _state = WaveState.Spawning;
    }

    private void SpawnNextEnemy()
    {
        _enemySpawner.Spawn();

        _spawnedInWave++;
    }

    private void NextWave()
    {
        _enemiesPerWave += 5;

        Debug.Log("Новые условия спавна");
    }
}