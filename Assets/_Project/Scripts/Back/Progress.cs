namespace _Project.Scripts.Back
{
    public struct ProgressSettings
    {
        public int MaxWaves;
        public int WaveIncrease;
        public int BaseEnemiesPerWave;
        public int BaseEnemiesIncrease;
        public int AddEnemies;
        public int AddEnemiesIncrease;
    }
    public class Progress
    {
        public int CurrentLevel {get; private set;}
        private readonly ProgressSettings _progressSettings;

        public Progress(int startLevel, ProgressSettings progressSettings)
        {
            _progressSettings = progressSettings;
            CurrentLevel = startLevel;
        }

        public void AdvanceLevel()
        {
            CurrentLevel++;
        }

        public int GetMaxWavesProgress()
        {
            return _progressSettings.MaxWaves + CurrentLevel * _progressSettings.WaveIncrease;
        }

        public int GetEnemiesPerWaveProgress()
        {
            return _progressSettings.BaseEnemiesPerWave + CurrentLevel * _progressSettings.BaseEnemiesIncrease;
        }

        public int GetAddEnemiesProgress()
        {
            return _progressSettings.AddEnemies + CurrentLevel * _progressSettings.AddEnemiesIncrease;
        }
    }
}