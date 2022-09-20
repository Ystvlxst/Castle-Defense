using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _hitEffect;

    private int _currentHealth;
    private int _lowestHealth = 25;

    public event Action<int> Damaged;

    public int Health => _health;
    public int CurrentHealth => _currentHealth;
    public int LowestHealth => _lowestHealth;

    private void Awake()
    {
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        Damaged?.Invoke(damage);
        _currentHealth -= damage;

        CheckLowHealth();

        _hitEffect.Play();
    }

    public void AddHealth(int health)
    {
        _currentHealth += health;

        if (_currentHealth >= _health)
            _currentHealth = _health;
    }

    public void CheckLowHealth()
    {
        if (_currentHealth <= _lowestHealth)
            _currentHealth = _lowestHealth;
    }
}
