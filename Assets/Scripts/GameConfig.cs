using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private int _towerMaxHealth;
    [SerializeField] private int _towerDamage;
    [SerializeField] private float _towerAttackRange;
    [SerializeField] private float _towerAttackSpeed;
    [SerializeField] private float _projectileSpeed;
    
    [SerializeField] private int _enemyMaxHealth;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private int _enemyDamage;
    
    [SerializeField] private int _maxWaves;
    [SerializeField] private int _enemiesPerWave;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _waveDelay;
    
    public int TowerMaxHealth => _towerMaxHealth;
    public int TowerDamage => _towerDamage;
    public float TowerAttackRange => _towerAttackRange;
    public float TowerAttackSpeed => _towerAttackSpeed;
    public float ProjectileSpeed => _projectileSpeed;

    public int EnemyMaxHealth => _enemyMaxHealth;
    public float EnemySpeed => _enemySpeed;
    public int EnemyDamage => _enemyDamage;
    
    public int MaxWaves => _maxWaves;
    public int EnemiesPerWave => _enemiesPerWave;
    public float SpawnTime => _spawnTime;
    public int WaveDelay => _waveDelay;
}
