using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class StatsManager : MonoBehaviour
    {
        public int DiedEnemyCount { get; private set; }
        
        public event Action OnEnemyValueChanged;
        
        public int EnemyKilled { get; private set; }
        public int WavesSurvived { get; private set; }
        
        private void Start()
        {
            Enemy.OnEnemyDied += AddKill;
            WaveManager.OnWaveFinished += AddWave;
        }

        private void OnDestroy()
        {
            Enemy.OnEnemyDied -= AddKill;
            WaveManager.OnWaveFinished -= AddWave;
        }

        private void AddKill(Enemy e)
        {
            EnemyKilled++;
            OnEnemyValueChanged?.Invoke();
        }

        private void AddWave()
        {
            WavesSurvived++;
        }

        public void ClearStats()
        {
            EnemyKilled = 0;
            WavesSurvived = 0;
        }

        public void UpdateDiedEnemies()
        {
            DiedEnemyCount++;
            OnEnemyValueChanged?.Invoke();
        }

        public void ClearDiedEnemies()
        {
            DiedEnemyCount = 0;
            OnEnemyValueChanged?.Invoke();
        }
    }
}