using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveState : State
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _speed;
    [SerializeField] private float _offset = 1.5f;

    private NavMeshAgent _navMeshAgent;
    private float _horizontalOffset;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
    }

    private void Start() => 
        _horizontalOffset = Random.Range(-0.5f, 0.5f);

    private void Update() => 
        _navMeshAgent.SetDestination(GetTargetPosition());

    private Vector3 GetTargetPosition()
    {
        if (_enemy.Target.FollowingEnemy == _enemy)
            return _enemy.Target.transform.position;

        float offset = GetOffset(_enemy.Target.FollowingEnemy);
        return _enemy.Target.transform.position + new Vector3(_horizontalOffset, 0, offset);
    }

    private float GetOffset(Enemy enemy)
    {
        if (enemy.FollowingEnemy == _enemy)
            return _offset;

        return GetOffset(enemy.FollowingEnemy) + _offset;
    }
}
