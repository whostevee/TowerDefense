using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private MoneyManager _moneyManager;
        [SerializeField] private TimeManager _timeManager;
        [SerializeField] private StatsManager _statsManager;
        [SerializeField] private Tower _tower;

        [SerializeField] private TextMeshProUGUI _textHealth;
        [SerializeField] private TextMeshProUGUI _textEnemies;
        [SerializeField] private TextMeshProUGUI _textWaves;
        [SerializeField] private TextMeshProUGUI _textMoney;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private GameObject _pauseMenuPanel;

        private int _currentWave;
        private int _maxWaves;
        private int _diedEnemies;
        private int _enemiesInWave;

        private int _towerHealth;
        private int _towerMaxHealth;
        private int _moneyCount;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(PauseGame);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(PauseGame);
        }

        private void Start()
        {
            _moneyManager.Wallet.OnMoneyChanged += UpdateMoney;
            _statsManager.Statistics.OnEnemyValueChanged += UpdateEnemies;

            _tower.TowerHealth.OnHealthChanged += UpdateTowerHealth;
            _waveManager.OnEnemiesInWaveChanged += UpdateEnemies;
            _waveManager.OnCurrentWaveChanged += UpdateWaves;
            
            UpdateTowerHealth(_tower.Health, _tower.MaxHealth);
        }

        private void OnDestroy()
        {
            _moneyManager.Wallet.OnMoneyChanged -= UpdateMoney;
            _statsManager.Statistics.OnEnemyValueChanged -= UpdateEnemies;

            _tower.TowerHealth.OnHealthChanged -= UpdateTowerHealth;
            _waveManager.OnEnemiesInWaveChanged -= UpdateEnemies;
            _waveManager.OnCurrentWaveChanged -= UpdateWaves;
        }

        private void PauseGame()
        {
            _pauseMenuPanel.SetActive(true);

            gameObject.SetActive(false);
            _timeManager.Pause();
        }

        private void UpdateMoney()
        {
            _moneyCount = _moneyManager.Money;
            _textMoney.text = "Money: " + _moneyCount;
        }

        private void UpdateTowerHealth(int currentHealth, int maxHealth)
        {
            _textHealth.text = "HP: " + currentHealth + " / " + maxHealth;
        }

        private void UpdateEnemies()
        {
            _diedEnemies = _statsManager.CurrentWaveDiedEnemies;
            _enemiesInWave = _waveManager.EnemiesPerWave;

            _textEnemies.text = "Enemies: " + _diedEnemies + " / " + _enemiesInWave;
        }

        private void UpdateWaves()
        {
            _currentWave = _waveManager.WaveNumber;
            _maxWaves = _waveManager.MaxWaves;

            _textWaves.text = "Waves: " + _currentWave + " / " + _maxWaves;
        }
    }
}