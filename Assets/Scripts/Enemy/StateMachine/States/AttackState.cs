using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AttackState : State
{
    [SerializeField] private int _damage;

     private float _delay;

    private NavMeshAgent _navMeshAgent;
    private float _lastAttackTime;

    private Player _player;

    private void Start()
    {
        _delay = Random.Range(2, 4);

        _navMeshAgent = GetComponent<NavMeshAgent>();

        _player = FindObjectOfType<Player>();

        _navMeshAgent.speed = 0;
    }

    private void Update()
    {
        if(_lastAttackTime <= 0)
        {
            Attack(Target);
            _lastAttackTime = _delay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(EnemyTarget target)
    {
        _player.ApplyDamage(_damage);
    }
}
