using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _targetTransform;

    private NavMeshAgent _navMeshAgent;

    public Transform Target => _targetTransform;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
    }

    private void Start()
    {
        _navMeshAgent.SetDestination(_targetTransform.position);
    }
}
