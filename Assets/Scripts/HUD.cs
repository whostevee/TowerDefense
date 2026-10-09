using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class HUD : MonoBehaviour
{
   [SerializeField] private WaveManager _waveManager;
   [SerializeField] private Tower _tower;
   
   [SerializeField] private TextMeshProUGUI _textHealth;
   [SerializeField] private TextMeshProUGUI _textEnemies;
   [SerializeField] private TextMeshProUGUI _textWaves;
   
   private int _currentWave;
   private int _maxWaves;
   private int _enemiesAlive;
   private int _enemiesInWave;
   
   private int _towerHealth;
   private int _towerMaxHealth;

   private void Start()
   {
   }

   private void Update()
   {
      _currentWave = _waveManager.WaveNumber;
      _maxWaves = _waveManager.MaxWaves;
      _enemiesInWave = _waveManager.EnemiesPerWave;
      _enemiesAlive = GameManager.Instance.Enemies.Count;
      
      _towerMaxHealth = _tower.MaxHealth;
      _towerHealth = _tower.Health;
      
      _textHealth.text = "Health: " + _towerHealth + " / " + _towerMaxHealth;
      _textEnemies.text = "Enemies: " + _enemiesAlive + " / " + _enemiesInWave;
      _textWaves.text = "Waves: " + _currentWave + " / " + _maxWaves;
   }
}
