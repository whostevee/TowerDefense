using System;
using _Project.Scripts.Back;
using UnityEngine;

namespace _Project.Scripts
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private EnemyRegistry _enemyRegistry;
        [SerializeField] private WaveManager _waveManager;
        
        public Statistics Statistics {get; private set;}
        
        public int CurrentWaveDiedEnemies => Statistics.CurrentWaveDiedEnemies;
        public int TotalEnemyKilled => Statistics.TotalEnemyKilled;
        public int WavesSurvived => Statistics.WavesSurvived;

        private void Awake()
        {
            Statistics = new Statistics();
        }
        private void Start()
        {
            _enemyRegistry.OnEnemyDied += UpdateDiedEnemies;
            _waveManager.OnWaveFinished += AddWave;
            _waveManager.OnCurrentWaveChanged += ClearDiedEnemies;
        }

        private void OnDestroy()
        {
            _enemyRegistry.OnEnemyDied -= UpdateDiedEnemies;
            _waveManager.OnWaveFinished -= AddWave;
            _waveManager.OnCurrentWaveChanged -= ClearDiedEnemies;
        }

        private void AddWave()
        {
            Statistics.AddWave();
        }

        private void UpdateDiedEnemies()
        {
            Statistics.UpdateDiedEnemies();
        }

        private void ClearDiedEnemies()
        {
            Statistics.ClearCurrentDiedEnemies();
        }
    }
}