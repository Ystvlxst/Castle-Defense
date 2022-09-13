using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _hitEffect;

    private int _currentHealth;

    public event UnityAction Die;
    public event UnityAction Damaged;

    public int Health => _health;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        Damaged?.Invoke();
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Dying();

        _hitEffect.Play();
    }

    public void AddHealth(int health)
    {
        _currentHealth += health;

        if (_currentHealth >= _health)
            _currentHealth = _health;
    }

    private void Dying()
    {
        Die?.Invoke();
    }
}
