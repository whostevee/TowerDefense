using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace _Project.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        private const string Walking = "isWalking";
        private const string Idle = "isIdle";
        private const string Dead = "Die";
        private const float MovementThreshold = 0.1f;
        private const float EnemyDestroyTime = 0.5f;

        [SerializeField] private EnemyConfig _enemyConfig;

        [SerializeField] private string[] _attackAnimations =
            { "Attack 1", "Attack 2", "Attack 3", "Attack 4" };

        private float _speed;
        private int _damage;
        private int _health;
        private int _maxHealth;
        private bool _isDead;
        private bool _isCanAttack;
        private float _accumulator;
        private float _attackDelay;

        private Tower _target;
        private NavMeshAgent _agent;
        private Animator _animator;

        public event Action<Enemy> OnDied; 

        public int Health => _health;
        public int MaxHealth => _maxHealth;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _maxHealth = _enemyConfig.EnemyMaxHealth;
            _health = _maxHealth;
            _speed = _enemyConfig.EnemySpeed;
            _damage = _enemyConfig.EnemyDamage;
            _attackDelay = _enemyConfig.EnemyAttackDelay;
        
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_isDead)
            {
                return;
            }
        
            if (_target == null)
            {
                _agent.isStopped = true;
                _animator.SetBool(Walking, false);
                _animator.SetBool(Idle, true);
                return;
            }

            if (!_isCanAttack)
            {
                Move();
            }

            if (_isCanAttack && TakeTimer(ref _accumulator, _attackDelay))
            {
                _animator.SetBool(Walking, false);
                _animator.SetBool(Idle, false);

                Attack();
                _animator.SetBool(Idle, true);
            }
        }

        private void Move()
        {
            _agent.speed = _speed;

            if (_agent.velocity.magnitude > MovementThreshold)
            {
                _animator.SetBool(Walking, true);
            }
            else
            {
                _animator.SetBool(Walking, false);
            }

            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                _animator.SetBool(Walking, false);
                _animator.SetBool(Idle, true);
                _isCanAttack = true;
            }
        }

        private void Attack()
        {
            _animator.SetBool(Walking, false);
            _animator.SetBool(Idle, false);
        
            _animator.Play(_attackAnimations[Random.Range(0, _attackAnimations.Length)]);
            _target.TakeDamage(_damage);
        }

        public void SetTarget(Tower target)
        {
            _target = target;

            Vector2 circle = Random.insideUnitCircle.normalized;
            Vector3 offset = new Vector3(circle.x, 0, circle.y);

            if (_target != null)
            {
                _agent.SetDestination(_target.transform.position + offset);
            }
        }

        public void TakeDamage(int damage)
        {
            if (_isDead)
            {
                return;
            }

            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            _agent.isStopped = true;

            _animator.SetTrigger(Dead);

            OnDied?.Invoke(this);

            Destroy(gameObject, EnemyDestroyTime);
        }

        private bool TakeTimer(ref float accumulator, float delay)
        {
            accumulator += Time.deltaTime;
            if (accumulator < delay)
            {
                return false;
            }

            accumulator = 0;
            return true;
        }
    }
}