using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveState : State
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _speed;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_enemy.Target.transform.position);
    }
}
