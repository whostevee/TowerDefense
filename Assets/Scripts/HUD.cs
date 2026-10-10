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
   
   [SerializeField] private Button _pauseButton;
   [SerializeField] private GameObject _pauseMenuPanel;

   private CanvasGroup _canvasGroup;
   private int _currentWave;
   private int _maxWaves;
   private int _enemiesAlive;
   private int _enemiesInWave;
   
   private int _towerHealth;
   private int _towerMaxHealth;

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
      _canvasGroup = _pauseMenuPanel.GetComponent<CanvasGroup>();
   }

   private void Update()
   {
      _currentWave = _waveManager.WaveNumber;
      _maxWaves = _waveManager.MaxWaves;
      _enemiesInWave = _waveManager.EnemiesPerWave;
      _enemiesAlive = GameManager.Instance.Enemies.Count;
      
      _towerMaxHealth = _tower.MaxHealth;
      _towerHealth = _tower.Health;
      
      _textHealth.text = "HP: " + _towerHealth + " / " + _towerMaxHealth;
      _textEnemies.text = "Enemies: " + _enemiesAlive + " / " + _enemiesInWave;
      _textWaves.text = "Waves: " + _currentWave + " / " + _maxWaves;

   }

   private void PauseGame()
   {
      _pauseMenuPanel.SetActive(true);
      Time.timeScale = 0f;
   }
}
