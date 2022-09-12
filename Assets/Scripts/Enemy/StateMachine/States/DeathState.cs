using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DeathState : State
{
    private const string _dead = "Dead";

    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private LootDrop _lootDrop;
    [SerializeField] private ParticleSystem _deadEffect;
    [SerializeField] private Rigidbody _rigidbody;

    private void OnEnable()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        _navMeshAgent.speed = 0;
        _rigidbody.isKinematic = false;
        _animator.SetTrigger(_dead);
        _lootDrop.DropLoot();

        yield return new WaitForSeconds(1);

        _deadEffect.Play();
        //_navMeshAgent.baseOffset = Mathf.Lerp(1, -0.6f, 1);
        Destroy(gameObject, 1);
    }
}