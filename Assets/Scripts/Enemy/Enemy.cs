using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _reward;
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private LootDrop _lootDrop;
    [SerializeField] private Canvas _healthCanvas;
    [SerializeField] private Rigidbody _rootRigidbody;
    [SerializeField] private Rigidbody[] _ragdollRigidbodyes;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private ParticleSystem _shotEffect;

    private EnemyTarget _target;
    private Coroutine _coroutine;

    public EnemyTarget Target => _target;
    public int Reward => _reward;
    public float Health => _health;
    public bool IsDying => _health <= 0;

    public event UnityAction<Enemy> Dying;

    public void Init(EnemyTarget target)
    {
        _target = target;
        _enemyStateMachine.Launch();

        _rootRigidbody.isKinematic = false;

        foreach (Rigidbody rigidbody in _ragdollRigidbodyes)
            rigidbody.isKinematic = true;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(HealthView());
    }

    public void TakeImpulseForce(float force)
    {
        foreach (Rigidbody rigidbody in _ragdollRigidbodyes)
            rigidbody.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    private void Die()
    {
        _animator.enabled = false;
        _rootRigidbody.isKinematic = true;
        _healthCanvas.gameObject.SetActive(false);
        _navMeshAgent.speed = 0;
        _shotEffect.Stop();

        foreach (Rigidbody rigidbody in _ragdollRigidbodyes)
            rigidbody.isKinematic = false;

        _lootDrop.DropLoot();
        Dying?.Invoke(this);

        Destroy(gameObject, 1);
    }

    private IEnumerator HealthView()
    {
        _healthCanvas.enabled = true;
        yield return new WaitForSeconds(1);
        _healthCanvas.enabled = false;
    }
}
