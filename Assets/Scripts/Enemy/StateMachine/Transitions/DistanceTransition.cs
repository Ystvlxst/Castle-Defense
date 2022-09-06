using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

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
        if (Vector3.Distance(transform.position, _enemy.Target.transform.position) < _transitionRange)
            NeedTransit = true;
    }
}
