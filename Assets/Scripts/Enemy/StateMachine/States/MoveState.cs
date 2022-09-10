using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveState : State
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        RaycastHit[] raycastHits = new RaycastHit[1];
        var target = GetTargetPosition();

        if (Physics.RaycastNonAlloc(transform.position + Vector3.up, transform.forward, raycastHits, 1.5f, _layerMask) != 0)
            target = transform.position;
        
        if(_navMeshAgent.isOnNavMesh)
            _navMeshAgent.SetDestination(target);
    }

    private Vector3 GetTargetPosition() => 
        _enemy.Target.transform.position;
}
