using UnityEngine;

namespace _Project.Scripts
{
    public class ProgressManager : MonoBehaviour
    {
        [SerializeField] private ProgressConfig _progressConfig;
        private int _currentLevel;

        private void Start()
        {
            _currentLevel = PlayerPrefs.GetInt("Level", 0);
            PlayerPrefs.DeleteKey("Level");
            PlayerPrefs.Save();
        }

        public void AdvanceLevel()
        {
            _currentLevel++;
            PlayerPrefs.SetInt("Level", _currentLevel);
            PlayerPrefs.Save();
        }

        public int GetMaxWavesProgress()
        {
            return _progressConfig.GetMaxWaves(_currentLevel);
        }

        public int GetEnemiesPerWaveProgress()
        {
            return _progressConfig.GetEnemiesPerWave(_currentLevel);
        }
        
        public int GetAddEnemiesProgress()
        {
            return _progressConfig.GetAddEnemies(_currentLevel);
            
        }
    }
}