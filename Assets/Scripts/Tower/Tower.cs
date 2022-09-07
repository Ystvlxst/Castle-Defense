using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private List<PartTower> _towerParts;

    private int _currentHealth;
    private int _lossFactor;

    public event UnityAction Die;

    public int Health => _health;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _health;

        _lossFactor = 1000;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Dying();

        _hitEffect.Play();
        CheckLoss();
    }

    private void Dying()
    {
        Die?.Invoke();
    }

    private void CheckLoss()
    {
        if (_currentHealth <= _health - _lossFactor)
        {
            foreach(PartTower partTower in _towerParts)
                partTower.DestroyPart();

            _lossFactor += _lossFactor;
        }
    }
}
