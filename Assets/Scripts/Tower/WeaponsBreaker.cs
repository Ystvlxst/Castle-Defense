using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assistants.Behaviour;
using UnityEngine;

public class WeaponsBreaker : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    
    private readonly List<BreakdownStatus> _weapons = new List<BreakdownStatus>();
    private int _damage;
    private int _damageToBreakWeapon = 500;
    private int _maxBrokenWeaponCount = 2;

    private void OnEnable() => 
        _tower.Damaged += OnTowerDamaged;

    private void OnDisable() => 
        _tower.Damaged -= OnTowerDamaged;

    public void Add(BreakdownStatus reference) => 
        _weapons.Add(reference);

    private void OnTowerDamaged(int damage)
    {
        if(_weapons.Count(weapon => weapon.Broken) >= _maxBrokenWeaponCount)
            return;
        
        _damage += damage;

        if (_damage >= _damageToBreakWeapon)
        {
            var first = _weapons.Where(weapon => weapon.Broken == false).Shuffle().FirstOrDefault();

            if (first != null)
            {
                first.Break();
                _damage = 0;
            }
        }
    }
}