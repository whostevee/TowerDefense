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
    [SerializeField] private float _enemyAttackDelay;
    
    [SerializeField] private int _maxWaves;
    [SerializeField] private int _enemiesPerWave;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _waveDelay;
    
    [SerializeField] private int _maxDamageUpgrade;
    [SerializeField] private int _maxAttackSpeedUpgrade;
    [SerializeField] private int _maxHealthUpgrade;
    [SerializeField] private int _damageUpgradePrice;
    [SerializeField] private int _healthUpgradePrice;
    [SerializeField] private int _attackSpeedUpgradePrice;
    
    [SerializeField] private int _healthUpgradeQuantity;
    [SerializeField] private float _attackSpeedUpgradeQuantity;
    [SerializeField] private int _damageUpgradeQuantity;
    
    public int TowerMaxHealth => _towerMaxHealth;
    public int TowerDamage => _towerDamage;
    public float TowerAttackRange => _towerAttackRange;
    public float TowerAttackSpeed => _towerAttackSpeed;
    public float ProjectileSpeed => _projectileSpeed;

    public int EnemyMaxHealth => _enemyMaxHealth;
    public float EnemySpeed => _enemySpeed;
    public int EnemyDamage => _enemyDamage;
    public float EnemyAttackDelay => _enemyAttackDelay;
    
    public int MaxWaves => _maxWaves;
    public int EnemiesPerWave => _enemiesPerWave;
    public float SpawnTime => _spawnTime;
    public int WaveDelay => _waveDelay;
    
    public int MaxDamageUpgrade => _maxDamageUpgrade;
    public int MaxHealthUpgrade => _maxHealthUpgrade;
    public int MaxAttackSpeedUpgrade => _maxAttackSpeedUpgrade;
    public int DamageUpgradePrice => _damageUpgradePrice;
    public int HealthUpgradePrice => _healthUpgradePrice;
    public int AttackSpeedUpgradePrice => _attackSpeedUpgradePrice;
    
    public int HealthUpgradeQuantity => _healthUpgradeQuantity;
    public float AttackSpeedUpgradeQuantity => _attackSpeedUpgradeQuantity;
    public int DamageUpgradeQuantity => _damageUpgradeQuantity;

}
