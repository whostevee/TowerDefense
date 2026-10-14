using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private string[] _attackAnimations =
        { "Attack 1", "Attack 2", "Attack 3", "Attack 4" };

    private float _speed;
    private int _damage;
    private int _health;
    private int _maxHealth;
    private bool _isDead;

    private Tower _target;
    private NavMeshAgent _agent;
    private Animator _animator;

    public static event Action<Enemy> OnEnemyDied;
    
    public int Health => _health;
    public int MaxHealth => _maxHealth;
    
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _maxHealth = _gameConfig.EnemyMaxHealth;
        _health = _maxHealth;
        _speed = _gameConfig.EnemySpeed;
        _damage = _gameConfig.EnemyDamage;
        
        _target = FindAnyObjectByType<Tower>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        _agent.speed = _speed;

        if (_agent.velocity.magnitude > 0.1f)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isIdle", false);
        }

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetBool("isWalking", false);

        var animationState = _animator.GetCurrentAnimatorStateInfo(0);

        if (animationState.normalizedTime < 1f)
        {
            return;
        }

        _animator.Play(_attackAnimations[Random.Range(0, _attackAnimations.Length)]);
        _target.TakeDamage(_damage);
    }

    public void SetTarget(Tower target)
    {
        _target = target;

        Vector2 circle = Random.insideUnitCircle.normalized;
        Vector3 offset = new Vector3(circle.x, 0, circle.y);
        _agent.SetDestination(_target.transform.position + offset);
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
        
        _animator.SetTrigger("Die");
        
        OnEnemyDied?.Invoke(this);
        
        Destroy(gameObject, 0.5f);
    }
}