using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
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

        GameManager.Instance.OnMoneyChanged += UpdateMoney;
        GameManager.Instance.OnEnemyValueChanged += UpdateEnemies;

        _tower.OnTowerHealthChanged += UpdateTowerHealth;
        _waveManager.OnEnemiesInWaveChanged += UpdateEnemies;
        _waveManager.OnCurrentWaveChanged += UpdateWaves;
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(PauseGame);

        GameManager.Instance.OnMoneyChanged -= UpdateMoney;
        GameManager.Instance.OnEnemyValueChanged -= UpdateEnemies;

        _tower.OnTowerHealthChanged -= UpdateTowerHealth;
        _waveManager.OnEnemiesInWaveChanged -= UpdateEnemies;
        _waveManager.OnCurrentWaveChanged -= UpdateWaves;
    }

    private void PauseGame()
    {
        _pauseMenuPanel.SetActive(true);

        gameObject.SetActive(false);
        GameManager.Instance.PauseGame();
    }

    private void UpdateMoney()
    {
        _moneyCount = GameManager.Instance.Money;
        _textMoney.text = "Money: " + _moneyCount;
    }

    private void UpdateTowerHealth()
    {
        _towerMaxHealth = _tower.MaxHealth;
        _towerHealth = _tower.Health;

        _textHealth.text = "HP: " + _towerHealth + " / " + _towerMaxHealth;
    }

    private void UpdateEnemies()
    {
        _diedEnemies = GameManager.Instance.DiedEnemyCount;
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