using _Project.Scripts.Back;
using UnityEngine;

namespace _Project.Scripts
{
    public class ProgressManager : MonoBehaviour
    {
        [SerializeField] private ProgressConfig _progressConfig;
        private int _currentLevel;
        private Progress _progress;

        private void Start()
        {
            _currentLevel = PlayerPrefs.GetInt("Level", 0);

            var settings = new ProgressSettings
            {
                MaxWaves = _progressConfig.MaxWaves, WaveIncrease = _progressConfig.WaveIncrease,
                BaseEnemiesPerWave = _progressConfig.BaseEnemiesPerWave,
                BaseEnemiesIncrease = _progressConfig.BaseEnemiesIncrease,
                AddEnemies = _progressConfig.AddEnemies, AddEnemiesIncrease = _progressConfig.AddEnemiesIncrease
            };
            _progress = new Progress(_currentLevel, settings);
            
            PlayerPrefs.DeleteKey("Level");
            PlayerPrefs.Save();
        }

        public void AdvanceLevel()
        {
            _progress.AdvanceLevel();
            PlayerPrefs.SetInt("Level", _progress.CurrentLevel);
            PlayerPrefs.Save();
        }
        
        public int GetMaxWavesProgress()
        {
            return _progress.GetMaxWavesProgress();
        }

        public int GetEnemiesPerWaveProgress()
        {
            return _progress.GetEnemiesPerWaveProgress();
        }

        public int GetAddEnemiesProgress()
        {
            return _progress.GetAddEnemiesProgress();
        }
    }
}