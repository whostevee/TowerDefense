using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class Tower : MonoBehaviour, IDamageable, IPointerClickHandler
    {
        private const float UpdateTime = 0.5f;
        private const int PointsCount = 50;

        [SerializeField] private EnemyRegistry _enemyRegistry;
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

        private Enemy _nearestEnemy;
        private float _nearestDistance = Mathf.Infinity;

        private LineRenderer _lineRenderer;

        public static Action OnTowerClicked;
        public static Action OnTowerDestroyed;
        public event Action OnTowerHealthChanged;

        public int Health => _health;
        public int MaxHealth => _maxHealth;

        private void OnEnable()
        {
            _upgradeMenu.OnTowerUpgraded += TakeUpgrade;
        }

        private void OnDisable()
        {
            _upgradeMenu.OnTowerUpgraded -= TakeUpgrade;
        }

        private void Start()
        {
            _maxHealth = _towerConfig.TowerMaxHealth;
            _health = _maxHealth;
            OnTowerHealthChanged?.Invoke();

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
                _attackTimeAcc = _shotDelayTime;
            }
            else if (TakeTimer(ref _attackTimeAcc, _shotDelayTime))
            {
                Attack(_nearestEnemy);
            }
        }

        private void Attack(Enemy enemy)
        {
            if (enemy.Health > 0)
            {
                var projectile = Instantiate(_projectile, _firePoint.position, Quaternion.identity);
                projectile.InitializeProjectile(_attackRange, _damage, _projectileSpeed, enemy);
            }
        }

        private Vector3[] SetPoints(int pointsCount)
        {
            var pointsPositions = new Vector3[pointsCount];

            for (var i = 0; i < pointsCount; i++)
            {
                var angle = (float)i / pointsCount * (Mathf.PI * 2);
                var x = Mathf.Cos(angle) * _attackRange;
                var z = Mathf.Sin(angle) * _attackRange;
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
                return;
            }

            OnTowerHealthChanged?.Invoke();
        }

        private void FindTarget()
        {
            _nearestDistance = Mathf.Infinity;
            _nearestEnemy = null;

            foreach (Enemy enemy in _enemyRegistry.Enemies)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist <= _attackRange && dist < _nearestDistance)
                {
                    _nearestEnemy = enemy;
                    _nearestDistance = dist;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnTowerClicked?.Invoke();
        }

        private void TakeUpgrade(UpgradeMenu.UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case UpgradeMenu.UpgradeType.Health:
                    _health += _healthUpgradeQuantity;
                    _maxHealth += _healthUpgradeQuantity;
                    OnTowerHealthChanged?.Invoke();
                    break;
                case UpgradeMenu.UpgradeType.AttackSpeed:
                    _shotDelayTime -= _attackSpeedUpgradeQuantity;
                    break;
                case UpgradeMenu.UpgradeType.Damage:
                    _damage += _damageUpgradeQuantity;
                    break;
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
    }
}