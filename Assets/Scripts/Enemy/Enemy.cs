using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _reward;
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private LootDrop _lootDrop;
    [SerializeField] private Canvas _healthCanvas;

    private EnemyTarget _target;
    private Coroutine _coroutine;

    public EnemyTarget Target => _target;
    public int Reward => _reward;
    public float Health => _health;

    public event UnityAction<Enemy> Dying;

    public void Init(EnemyTarget target)
    {
        _target = target;
        _enemyStateMachine.Launch();
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

    private void Die()
    {
        Dying?.Invoke(this);
        Destroy(gameObject);
        _lootDrop.DropLoot();
    }

    private IEnumerator HealthView()
    {
        _healthCanvas.enabled = true;
        yield return new WaitForSeconds(1);
        _healthCanvas.enabled = false;
    }
}
