using System;
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
   }

   private void OnDisable()
   {
      _pauseButton.onClick.RemoveListener(PauseGame);
   }

   private void Update()
   {
      _currentWave = _waveManager.WaveNumber;
      _maxWaves = _waveManager.MaxWaves;
      _enemiesInWave = _waveManager.EnemiesPerWave;
      _diedEnemies = GameManager.Instance.DiedEnemyCount;
      
      _towerMaxHealth = _tower.MaxHealth;
      _towerHealth = _tower.Health;
      _moneyCount = GameManager.Instance.Money;
      
      _textHealth.text = "HP: " + _towerHealth + " / " + _towerMaxHealth;
      _textEnemies.text = "Enemies: " + _diedEnemies + " / " + _enemiesInWave;
      _textWaves.text = "Waves: " + _currentWave + " / " + _maxWaves;
      _textMoney.text = "Money: " + _moneyCount;
   }

   private void PauseGame()
   {
      _pauseMenuPanel.SetActive(true);
      
      gameObject.SetActive(false);
      Time.timeScale = 0f;
   }
}
