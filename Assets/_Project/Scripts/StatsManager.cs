using UnityEngine;

namespace _Project.Scripts
{
    public class StatsManager : MonoBehaviour
    {
        public static StatsManager Instance { get; private set; }

        public int EnemyKilled { get; private set; }
        public int WavesSurvived { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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
    }
}