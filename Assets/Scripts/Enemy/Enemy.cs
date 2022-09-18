using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private Health _health;

    private EnemyTarget _target;

    public EnemyTarget Target => _target;
    public IDamageable Damageable => _health;
    public bool IsDying => _health.Dead;

    public event UnityAction<Enemy> Dying;

    public void Init(EnemyTarget target)
    {
        _health.enabled = true;
        _target = target;
        _enemyStateMachine.Launch();
    }

    private void Awake() => 
        _health.enabled = false;

    private void OnEnable() => 
        _health.Died += OnDied;

    private void OnDisable() => 
        _health.Died -= OnDied;

    private void OnDied(Health _) => 
        Dying?.Invoke(this);
}
