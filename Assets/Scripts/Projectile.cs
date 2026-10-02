using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 35;
    private float _maxDistance;
    private Vector3 _spawnPosition;
    
    private Enemy _enemyTarget;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        var direction = (_enemyTarget.transform.position - transform.position).normalized;
        _rigidbody.linearVelocity = direction * _speed;
        
        _maxDistance = FindAnyObjectByType<Tower>().AttackRange;
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