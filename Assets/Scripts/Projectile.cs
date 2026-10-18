using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private int _damage;
    private float _maxDistance;
    
    private Vector3 _spawnPosition;

    private Enemy _enemyTarget;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _maxDistance = FindAnyObjectByType<Tower>().AttackRange;
        _damage = FindAnyObjectByType<Tower>().Damage;
        _speed = FindAnyObjectByType<Tower>().ProjectileSpeed;
        
        _spawnPosition = transform.position;
        
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _spawnPosition) >= _maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Enemy target)
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
}