using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private enum WaveState
    {
        Spawning,
        WaitingForClear,
        Pause,
        Stop,
        Win
    }

    private const int AddEnemies = 5;

    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private EnemySpawner enemySpawner;

    private WaveState _state;

    private int _maxWaves;
    private int _enemiesPerWave;
    private float _spawnTime;
    private int _waveDelay;
    private int _spawnedInWave;

    private float _nextSpawnTime;
    private float _waveEndTime;
    private int _waveNumber = 1;

    public static Action OnWavesEnded;
    public static Action OnWaveFinished;
    public event Action OnEnemiesInWaveChanged;
    public event Action OnCurrentWaveChanged;

    public int MaxWaves => _maxWaves;
    public int EnemiesPerWave => _enemiesPerWave;
    public int WaveNumber => _waveNumber;

    private void OnEnable()
    {
        UIManager.OnGameStarted += StartWave;
    }

    private void OnDisable()
    {
        UIManager.OnGameStarted -= StartWave;
    }

    private void Start()
    {
        _maxWaves = enemyConfig.MaxWaves;
        _enemiesPerWave = enemyConfig.EnemiesPerWave;
        OnEnemiesInWaveChanged?.Invoke();

        _spawnTime = enemyConfig.SpawnTime;
        _waveDelay = enemyConfig.WaveDelay;

        _state = WaveState.Stop;
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
            case WaveState.Stop:
                UpdateStop();
                break;
            case WaveState.Win:
                OnWavesEnded?.Invoke();
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

            OnWaveFinished?.Invoke();
            _state = WaveState.Pause;
        }
    }

    private void UpdatePause()
    {
        if (_waveNumber >= _maxWaves)
        {
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

    private void UpdateStop()
    {
        Time.timeScale = 0;
    }

    private void StartWave()
    {
        _spawnedInWave = 0;
        GameManager.Instance.ClearDiedEnemies();

        _nextSpawnTime = Time.time + _spawnTime;
        _state = WaveState.Spawning;

        OnCurrentWaveChanged?.Invoke();
    }

    private void SpawnNextEnemy()
    {
        enemySpawner.Spawn();

        _spawnedInWave++;
    }

    private void NextWave()
    {
        _enemiesPerWave += AddEnemies;
        OnEnemiesInWaveChanged?.Invoke();
    }
}