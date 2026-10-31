using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private EnemyRegistry _enemyRegistry;
        
        public event Action OnEnemyValueChanged;
        
        public int DiedEnemyCount { get; private set; }
        public int TotalEnemyKilled { get; private set; }
        public int WavesSurvived { get; private set; }
        
        private void Start()
        {
            _enemyRegistry.OnEnemyDied += UpdateDiedEnemies;
            WaveManager.OnWaveFinished += AddWave;
        }

        private void OnDestroy()
        {
            _enemyRegistry.OnEnemyDied -= UpdateDiedEnemies;
            WaveManager.OnWaveFinished -= AddWave;
        }

        private void AddWave()
        {
            WavesSurvived++;
        }

        private void UpdateDiedEnemies()
        {
            DiedEnemyCount++;
            TotalEnemyKilled++;
            OnEnemyValueChanged?.Invoke();
        }

        public void ClearDiedEnemies()
        {
            DiedEnemyCount = 0;
            OnEnemyValueChanged?.Invoke();
        }
    }
}