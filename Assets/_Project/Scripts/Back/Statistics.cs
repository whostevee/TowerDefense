using System;

namespace _Project.Scripts.Back
{
    public class Statistics
    {
        public int WavesSurvived { get; private set; }
        public int TotalEnemyKilled { get; private set; }
        public int CurrentWaveDiedEnemies { get; private set; }

        public event Action OnEnemyValueChanged;

        public void AddWave()
        {
            WavesSurvived++;
        }

        public void UpdateDiedEnemies()
        {
            TotalEnemyKilled++;
            CurrentWaveDiedEnemies++;
            OnEnemyValueChanged?.Invoke();
        }

        public void ClearCurrentDiedEnemies()
        {
            CurrentWaveDiedEnemies = 0;
            OnEnemyValueChanged?.Invoke();
        }
    }
}