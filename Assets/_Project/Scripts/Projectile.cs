using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        private float _speed;
        private int _damage;
        private float _maxDistance;
        private bool _wasPaused;

        private Vector3 _spawnPosition;
        private Vector3 _projectileVelocity;

        private Enemy _enemyTarget;
        private Rigidbody _rigidbody;
        private TimeManager _timeManager;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnDestroy()
        {
            if (_timeManager != null)
            {
                _timeManager.OnGamePaused -= SaveVelocity;
            }
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _spawnPosition) >= _maxDistance)
            {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (_timeManager.IsPaused)
            {
                _rigidbody.isKinematic = true;
                _wasPaused = true;
            }
            else if (!_timeManager.IsPaused && _wasPaused)
            {
                _rigidbody.isKinematic = false;
                _rigidbody.linearVelocity = _projectileVelocity;
                _wasPaused = false;
            }
        }

        private void SetTarget(Enemy target)
        {
            _enemyTarget = target;

            var direction = (_enemyTarget.transform.position - transform.position).normalized;
            _rigidbody.linearVelocity = direction * _speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }

        public void InitializeProjectile(float attackRange, int damage, float speed, Enemy target,
            TimeManager timeManager)
        {
            _speed = speed;
            _damage = damage;
            _maxDistance = attackRange;
            _timeManager = timeManager;
            
            _timeManager.OnGamePaused += SaveVelocity;
            
            SetTarget(target);

            _spawnPosition = transform.position;
        }

        private void SaveVelocity()
        {
            if (_rigidbody.linearVelocity != Vector3.zero)
            {
                _projectileVelocity = _rigidbody.linearVelocity;
            }
        }
    }
}