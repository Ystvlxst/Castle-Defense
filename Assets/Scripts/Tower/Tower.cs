using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _hitEffect;

    private int _currentHealth;

    public event Action<int> Damaged;

    public int Health => _health;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        Damaged?.Invoke(damage);
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            _currentHealth = 0;

        _hitEffect.Play();
    }

    public void AddHealth(int health)
    {
        _currentHealth += health;

        if (_currentHealth >= _health)
            _currentHealth = _health;
    }
}
