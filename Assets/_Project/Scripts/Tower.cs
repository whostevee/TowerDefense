using System;
using _Project.Scripts.Back;
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
        [SerializeField] private TimeManager _timeManager;
        private TowerUpgrades _towerUpgrades;
        private Timer _attackTimer;
        private Timer _searchTimer;

        private float _attackRange;
        private float _baseShotDelayTime;
        private int _baseDamage;
        private float _projectileSpeed;

        private Enemy _nearestEnemy;
        private float _nearestDistance = Mathf.Infinity;

        private LineRenderer _lineRenderer;

        public event Action OnTowerClicked;
        public event Action OnTowerDestroyed;

        public Health TowerHealth { get; private set; }
        public int Health => TowerHealth.CurrentHealth;
        public int MaxHealth => TowerHealth.MaxHealth;

        private void OnEnable()
        {
            _upgradeMenu.OnTowerUpgraded += TakeUpgrade;
        }

        private void OnDisable()
        {
            _upgradeMenu.OnTowerUpgraded -= TakeUpgrade;
        }

        private void Awake()
        {
            TowerHealth = new Health(_towerConfig.TowerMaxHealth);
            TowerHealth.OnDied += DestroyTower;
        }

        private void Start()
        {
            _baseShotDelayTime = _towerConfig.TowerShotDelay;
            _baseDamage = _towerConfig.TowerDamage;
            
            var towerSettings = new TowerUpgradesSettings()
            {
                ShotDelayUpgradeQuantity = _towerConfig.ShotDelayUpgradeQuantity,
                HealthUpgradeQuantity = _towerConfig.HealthUpgradeQuantity,
                DamageUpgradeQuantity =  _towerConfig.DamageUpgradeQuantity,
            };
            
            _towerUpgrades = new TowerUpgrades(towerSettings, TowerHealth, _baseShotDelayTime, _baseDamage);
            
            _attackTimer = new Timer(_towerUpgrades.ShotDelay);
            _searchTimer = new Timer(UpdateTime);
            
            _attackRange = _towerConfig.TowerAttackRange;
            _projectileSpeed = _towerConfig.ProjectileSpeed;

            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = PointsCount;
            _lineRenderer.SetPositions(SetPoints(PointsCount));
        }

        private void Update()
        {
            if (_timeManager.IsPaused)
            {
                return;
            }

            if (_searchTimer.Tick(Time.deltaTime))
            {
                FindTarget();
            }

            if (_nearestEnemy == null)
            {
                _attackTimer.SetReady();
            }
            else if (_attackTimer.Tick(Time.deltaTime))
            {
                Attack(_nearestEnemy);
            }
        }

        private void Attack(Enemy enemy)
        {
            if (enemy.Health > 0)
            {
                var projectile = Instantiate(_projectile, _firePoint.position, Quaternion.identity);

                projectile.InitializeProjectile(_attackRange, _towerUpgrades.Damage, _projectileSpeed, enemy, _timeManager);
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
            TowerHealth.TakeDamage(damage);
        }

        private void DestroyTower()
        {
            Destroy(gameObject);
            OnTowerDestroyed?.Invoke();

            TowerHealth.OnDied -= DestroyTower;
        }

        private void FindTarget()
        {
            _nearestDistance = Mathf.Infinity;
            _nearestEnemy = null;

            foreach (Enemy enemy in _enemyRegistry.Enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= _attackRange && distance < _nearestDistance)
                {
                    _nearestEnemy = enemy;
                    _nearestDistance = distance;
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
                    _towerUpgrades.TakeHealthUpgrade();
                    break;
                case UpgradeMenu.UpgradeType.AttackSpeed:
                    _towerUpgrades.TakeAttackSpeedUpgrade();
                    _attackTimer.Delay = _towerUpgrades.ShotDelay;
                    break;
                case UpgradeMenu.UpgradeType.Damage:
                    _towerUpgrades.TakeDamageUpgrade();
                    break;
            }
        }
    }
}