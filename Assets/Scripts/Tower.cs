using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _attackRange = 5f;
    private LineRenderer _lineRenderer;

    private float _updateTime = 0.5f;
    private int _pointsCount = 50;

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
                Attack(enemy);
            }
            else
            {
                return;
            }
        }
    }

    private void Attack(Enemy e)
    {
        Debug.Log("Tower attack - " + e);
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
}