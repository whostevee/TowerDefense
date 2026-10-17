using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IDamageable, IPointerClickHandler
{
    private const float UpdateTime = 0.5f;
    private const int PointsCount = 50;
    
    [SerializeField] private TowerConfig _towerConfig;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private UpgradeMenu _upgradeMenu;

    private float _attackRange;
    private int _health;
    private int _maxHealth;
    private float _shotDelayTime;
    private int _damage;
    private float _projectileSpeed;
    
    private int _healthUpgradeQuantity;
    private float _attackSpeedUpgradeQuantity;
    private int _damageUpgradeQuantity;
    
    private float _attackTimeAcc;
    private float _searchTimeAcc;
    private float _destroyTimeAcc;
    private bool _canShoot;

    private Enemy _nearestEnemy;
    private float _nearestDistance = Mathf.Infinity;

    private LineRenderer _lineRenderer;

    public static Action OnTowerClicked;
    public static Action OnTowerDestroyed;

    public int Damage => _damage;
    public int Health => _health;
    public int MaxHealth => _maxHealth;

    public float ProjectileSpeed => _projectileSpeed;
    public float AttackRange => _attackRange;

    private void OnEnable() => _upgradeMenu.OnTowerUpgraded += TakeUpgrade;
    private void OnDisable() => _upgradeMenu.OnTowerUpgraded -= TakeUpgrade;

    private void Start()
    {
        _maxHealth = _towerConfig.TowerMaxHealth;
        _health = _maxHealth;
        _attackRange = _towerConfig.TowerAttackRange;
        _shotDelayTime = _towerConfig.TowerAttackSpeed;
        _damage = _towerConfig.TowerDamage;
        _projectileSpeed = _towerConfig.ProjectileSpeed;

        _attackSpeedUpgradeQuantity = _towerConfig.AttackSpeedUpgradeQuantity;
        _healthUpgradeQuantity = _towerConfig.HealthUpgradeQuantity;
        _damageUpgradeQuantity = _towerConfig.DamageUpgradeQuantity;
        
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = PointsCount;
        _lineRenderer.SetPositions(SetPoints(PointsCount));
    }

    private void Update()
    {
        if (TakeTimer(ref _searchTimeAcc, UpdateTime))
        {
            FindTarget();
        }

        if (_nearestEnemy == null)
        {
            _canShoot = true;
            _attackTimeAcc = 0f;
        }
        else
        {
            if (_canShoot)
            {
                _canShoot = false;
                Attack(_nearestEnemy);
            }
            else if (TakeTimer(ref _attackTimeAcc, _shotDelayTime))
            {
                Attack(_nearestEnemy);
            }
        }
    }

    private void Attack(Enemy enemy)
    {
        var projectile = Instantiate(_projectile, _firePoint.position, Quaternion.identity);
        projectile.SetTarget(enemy);
    }

    private Vector3[] SetPoints(int pointsCount)
    {
        Vector3[] pointsPositions = new Vector3[pointsCount];

        for (int i = 0; i < pointsCount; i++)
        {
            float angle;
            float x;
            float z;

            angle = (float)i / pointsCount * (Mathf.PI * 2);
            x = Mathf.Cos(angle) * _attackRange;
            z = Mathf.Sin(angle) * _attackRange;
            pointsPositions[i] = new Vector3(x, 0, z) + transform.position;
        }

        return pointsPositions;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
            OnTowerDestroyed?.Invoke();
        }
    }

    private void FindTarget()
    {
        _nearestDistance = Mathf.Infinity;
        _nearestEnemy = null;

        foreach (Enemy enemy in GameManager.Instance.Enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= _attackRange && dist < _nearestDistance)
            {
                _nearestEnemy = enemy;
                _nearestDistance = dist;
            }
        }
    }

    private bool TakeTimer(ref float accumulator, float delay)
    {
        accumulator += Time.deltaTime;
        if (accumulator < delay)
        {
            return false;
        }

        accumulator = 0f;
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        OnTowerClicked?.Invoke();
    }

    private void TakeUpgrade(UpgradeMenu.UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeMenu.UpgradeType.Health:
                _health += _healthUpgradeQuantity;
                _maxHealth += _healthUpgradeQuantity;
                break;
            case UpgradeMenu.UpgradeType.AttackSpeed:
                _shotDelayTime -= _attackSpeedUpgradeQuantity;
                break;
            case UpgradeMenu.UpgradeType.Damage:
                _damage += _damageUpgradeQuantity;
                break;
        }
    }
}