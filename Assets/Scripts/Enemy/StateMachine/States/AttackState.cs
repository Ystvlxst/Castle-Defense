using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AttackState : State
{
    private const string _attack = "Attack";

    [SerializeField] private int _damage;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _shotEffect;

    private float _delay;

    private NavMeshAgent _navMeshAgent;
    private float _lastAttackTime;

    private Tower _player;

    private void Start()
    {
        _delay = Random.Range(2, 4);

        _navMeshAgent = GetComponent<NavMeshAgent>();

        _player = FindObjectOfType<Tower>();

        _navMeshAgent.speed = 0;
    }

    private void Update()
    {
        if(_lastAttackTime <= 0)
        {
            Attack();
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack()
    {
        _animator.SetTrigger(_attack);
        _shotEffect.Play();
        _player.ApplyDamage(_damage);
    }
}
