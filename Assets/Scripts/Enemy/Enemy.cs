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
    [SerializeField] private Collider[] _ragdollColliders;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private ParticleSystem _shotEffect;

    private EnemyTarget _target;
    private Coroutine _takeDamageCoroutine;
    private Coroutine _deathCoroutine;

    public EnemyTarget Target => _target;
    public int Reward => _reward;
    public float Health => _health;
    public bool IsDying => _health <= 0;
    public Rigidbody[] RigidBodies => _ragdollRigidbodyes;

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
        if (_health <= 0)
            return;
        
        _health -= damage;

        if (_takeDamageCoroutine != null)
            StopCoroutine(_takeDamageCoroutine);

        _takeDamageCoroutine = StartCoroutine(Damage(damage));

        if (_health <= 0)
            Die();
    }

    private IEnumerator Damage(float damage)
    {
        _healthCanvas.enabled = true;
        yield return new WaitForSeconds(1);
        _healthCanvas.enabled = false;
        _takeDamageCoroutine = null;
    }

    private void Die()
    {
        if (_deathCoroutine != null)
            StopCoroutine(_deathCoroutine);

        _deathCoroutine = StartCoroutine(Death());
    }

    private IEnumerator Death()
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

        yield return new WaitForSeconds(4);

        foreach (Collider collider in _ragdollColliders)
            collider.isTrigger = true;

        Destroy(gameObject, 2f);
        _deathCoroutine = null;
    }
}
