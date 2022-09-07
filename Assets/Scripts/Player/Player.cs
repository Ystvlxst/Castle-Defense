using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _currentHealth;

    public event UnityAction HealthChanged;
    public event UnityAction Die;

    public int Health => _health;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Dying();

        HealthChanged?.Invoke();
    }

    private void Dying()
    {
        Die?.Invoke();
    }
}
