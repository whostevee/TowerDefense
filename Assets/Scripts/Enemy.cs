using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string[] _attackAnimations =
        { "Attack 1", "Attack 2", "Attack 3", "Attack 4" };

    private Tower _target;
    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _target = FindObjectOfType<Tower>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
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
    }

    public void SetTarget(Tower target)
    {
        _target = target;

        Vector2 circle = Random.insideUnitCircle.normalized;
        Vector3 offset = new Vector3(circle.x, 0, circle.y);
        _agent.SetDestination(_target.transform.position + offset);
    }

    private void OnDestroy()
    {
        gameObject.SetActive(false);
        GameManager.Instance.RemoveEnemy(this);
    }
}