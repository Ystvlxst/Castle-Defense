using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private int _currentHealth;

    public int Money { get; private set; }

    private void Start()
    {
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
        
    }
}
