using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _loseCanvas;
    [SerializeField] private int _health = 100;

    private int _currentHealth;

    public int Health => _health;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _loseCanvas.SetActive(false);
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Dying();
    }

    private void Dying()
    {
        _loseCanvas.SetActive(true);
    }
}
