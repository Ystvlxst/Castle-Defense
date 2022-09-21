using System.Collections;
using DG.Tweening;
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
    
    private bool _thrown;
    private bool _destroying;

    private void OnEnable()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        _navMeshAgent.speed = 0;
        _animator.SetTrigger(_dead);
        _lootDrop.DropLoot();

        yield return new WaitForSeconds(1);

        _deadEffect.Play();
        yield return new WaitForSeconds(1);
        _destroying = true;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void Throw(Vector3 force)
    {
        if(!enabled || _thrown || _destroying)
            return;

        _thrown = true;
        
        transform.DORotate(-force, 0.5f);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}