using UnityEngine;

public class Tower : MonoBehaviour
{
    private const float UpdateTime = 0.5f;

    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private int _health = 1000;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _shotDelayTime = 2f;

    private float _attackTimeAcc;
    private float _searchTimeAcc;

    public float AttackRange => _attackRange;
    private Enemy _nearestEnemy;
    private float _nearestDistance = Mathf.Infinity;

    private int _pointsCount = 50;

    private LineRenderer _lineRenderer;


    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _pointsCount;
        _lineRenderer.SetPositions(SetPoints(_pointsCount));
    }

    private void Update()
    {
        if (!TakeTimer(ref _searchTimeAcc, UpdateTime))
        {
            return;
        }

        //TODO: разделить логику поиска и атаки
        FindTarget();
    }

    private void Attack(Enemy enemy)
    {
        var projectile = Instantiate(_projectile, _firePoint.position, Quaternion.identity);
        projectile.SetTarget(enemy);

        Debug.Log("Tower attack - " + enemy);
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
            Debug.Log("You lose");

            Destroy(gameObject);
        }
    }

    private void FindTarget()
    {
        Debug.Log("Ищу цель");
        _nearestDistance = Mathf.Infinity;
        _nearestEnemy = null;

        foreach (Enemy enemy in GameManager.Instance.Enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= _attackRange && dist < _nearestDistance)
            {
                _nearestEnemy = enemy;
                _nearestDistance = dist;
                Debug.Log("Цель найдена");
            }
        }

        if (_nearestEnemy == null)
        {
            return;
        }

        //TODO: разобараться с таймером
        //TODO: выстрел должен быть по таймеру, сейчас не так
        // if (!TakeTimer(ref _attackTimeAcc, _shotDelayTime))
        // {
        //     return;
        // }

        Debug.Log("Готов к атаке");
        Attack(_nearestEnemy);
    }

    private bool TakeTimer(ref float accumulator, float delay)
    {
        accumulator += Time.deltaTime;
        if (accumulator < delay)
        {
            return false;
        }

        Debug.Log("Таймер готов");
        accumulator = 0f;
        return true;
    }
}