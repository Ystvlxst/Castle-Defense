using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _amount;

    public event UnityAction<Health> Died;
    public event UnityAction<float> HealthChanged;
    
    public float Amount => _amount;
    public bool Dead => _amount <= 0;

    private void Start()
    {
        CheckDeath();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new InvalidOperationException();
        
        if (!enabled || _amount <= 0)
            return;
        
        _amount -= damage;
        HealthChanged?.Invoke(_amount);

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_amount <= 0)
            Died?.Invoke(this);
    }
}