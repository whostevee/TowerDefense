using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string[] _attackAnimations =
        { "Attack 1", "Attack 2", "Attack 3", "Attack 4" };

    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 15;

    private int _health = 100;
    private bool _isDead;

    private Tower _target;
    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
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
        
        GameManager.Instance.RemoveEnemy(this);
        
        Destroy(gameObject, 3f);
    }
}