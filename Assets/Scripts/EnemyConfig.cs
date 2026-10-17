using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private int _enemyMaxHealth;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private int _enemyDamage;
    
    [SerializeField] private int _maxWaves;
    [SerializeField] private int _enemiesPerWave;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _waveDelay;
    
    public int EnemyMaxHealth => _enemyMaxHealth;
    public float EnemySpeed => _enemySpeed;
    public int EnemyDamage => _enemyDamage;
    
    public int MaxWaves => _maxWaves;
    public int EnemiesPerWave => _enemiesPerWave;
    public float SpawnTime => _spawnTime;
    public int WaveDelay => _waveDelay;
    
    
}
