using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private int _health = 1000;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _shotTimer = 2f;

    public float AttackRange => _attackRange;

    private LineRenderer _lineRenderer;

    //private float _updateTime = 0.5f;
    private int _pointsCount = 50;
    private float _nextShotTime;
    

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _pointsCount;

        float[] points = new float[_pointsCount];

        _lineRenderer.SetPositions(SetPoints(points));
    }

    private void Update()
    {
        foreach (Enemy enemy in GameManager.Instance.Enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= _attackRange)
            {
                if (Time.time <= _nextShotTime)
                {
                    return;
                }
                Attack(enemy);
                
                _nextShotTime = Time.time + _shotTimer;
            }
            else
            {
                return;
            }
        }
    }

    private void Attack(Enemy enemy)
    {
        var projectile = Instantiate(_projectile, _firePoint.position, Quaternion.identity);
        projectile.SetTarget(enemy);
        
        Debug.Log("Tower attack - " + enemy);
    }

    private Vector3[] SetPoints(float[] points)
    {
        Vector3[] pointsPositions = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            float angle;
            float x;
            float z;

            angle = (float)i / points.Length * (Mathf.PI * 2);
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
}