using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _reward;
    [SerializeField] private Detail _detail;
    [SerializeField] private EnemyStateMachine _enemyStateMachine;

    private EnemyTarget _target;

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
    }

    private void Die()
    {
        Dying?.Invoke(this);
        Destroy(gameObject);
        var detail = Instantiate(_detail, _target.transform);
    }
}
