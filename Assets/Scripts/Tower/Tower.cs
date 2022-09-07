using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PartTower> _towerParts;

    private int _lossFactor;
    private int _currentHealth => _player.CurrentHealth;
    private int _health => _player.Health;

    private void OnEnable()
    {
        _lossFactor = 1000;

        _player.HealthChanged += CheckLoss;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= CheckLoss;
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
