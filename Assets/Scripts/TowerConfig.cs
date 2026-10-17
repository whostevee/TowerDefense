using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "Scriptable Objects/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    [SerializeField] private int _towerMaxHealth;
    [SerializeField] private int _towerDamage;
    [SerializeField] private float _towerAttackRange;
    [SerializeField] private float _towerAttackSpeed;
    [SerializeField] private float _projectileSpeed;
    
    [SerializeField] private int _healthUpgradeQuantity;
    [SerializeField] private float _attackSpeedUpgradeQuantity;
    [SerializeField] private int _damageUpgradeQuantity;
    
    [SerializeField] private int _maxDamageUpgrade;
    [SerializeField] private int _maxAttackSpeedUpgrade;
    [SerializeField] private int _maxHealthUpgrade;
    [SerializeField] private int _damageUpgradePrice;
    [SerializeField] private int _healthUpgradePrice;
    [SerializeField] private int _attackSpeedUpgradePrice;

    private float _damageMultiplier = 3f;
    private float _healthMultiplier = 2f;
    private float _attackSpeedMultiplier = 3.5f;
    private int _rounding = 10;
    
    public int TowerMaxHealth => _towerMaxHealth;
    public int TowerDamage => _towerDamage;
    public float TowerAttackRange => _towerAttackRange;
    public float TowerAttackSpeed => _towerAttackSpeed;
    public float ProjectileSpeed => _projectileSpeed;
    
    public int HealthUpgradeQuantity => _healthUpgradeQuantity;
    public float AttackSpeedUpgradeQuantity => _attackSpeedUpgradeQuantity;
    public int DamageUpgradeQuantity => _damageUpgradeQuantity;
    
    public int MaxDamageUpgrade => _maxDamageUpgrade;
    public int MaxHealthUpgrade => _maxHealthUpgrade;
    public int MaxAttackSpeedUpgrade => _maxAttackSpeedUpgrade;
    public int DamageUpgradePrice => _damageUpgradePrice;
    public int HealthUpgradePrice => _healthUpgradePrice;
    public int AttackSpeedUpgradePrice => _attackSpeedUpgradePrice;


    public int GetNewDamagePrice(int currentLevel)
    {
        float raw = _damageUpgradePrice * Mathf.Pow(_damageMultiplier, currentLevel);
        return Mathf.CeilToInt(raw / _rounding) * _rounding;
    }
    
    public int GetNewHealthPrice(int currentLevel)
    {
        float raw = _healthUpgradePrice * Mathf.Pow(_healthMultiplier, currentLevel);
        return Mathf.CeilToInt(raw / _rounding) * _rounding;
    }
    
    public int GetNewAttackSpeedPrice(int currentLevel)
    {
        float raw = _attackSpeedUpgradePrice * Mathf.Pow(_attackSpeedMultiplier, currentLevel);
        return Mathf.CeilToInt(raw / _rounding) * _rounding;
    }
    
    
}
