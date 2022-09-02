using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    private EnemyMovement _enemyMovement;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
    }

    private void Update()
    {
        CheckDistanceToTarget();
    }

    public void CheckDistanceToTarget()
    {
        if (Vector3.Distance(_enemyMovement.transform.position, _enemyMovement.Target.position) < _transitionRange)
            NeedTransit = true;
    }
}
