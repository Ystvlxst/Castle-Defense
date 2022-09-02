using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _loseCanvas;
    [SerializeField] private int _health = 100;

    private int _currentHealth;

    public int Money { get; private set; }

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

    public void AddMoney(int reward)
    {
        Money += reward;
    }

    private void Dying()
    {
        _loseCanvas.SetActive(true);
    }
}
