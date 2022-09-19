using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assistants.Behaviour;
using UnityEngine;

public class WeaponsBreaker : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private int _damageToBreakWeapon = 150;

    private readonly List<BreakdownStatus> _weapons = new List<BreakdownStatus>();
    private int _damage;
    private int _maxBrokenWeaponCount = 2;

    private void OnEnable() => 
        _tower.Damaged += OnTowerDamaged;

    private void OnDisable() => 
        _tower.Damaged -= OnTowerDamaged;

    public void Add(BreakdownStatus reference) => 
        _weapons.Add(reference);

    private void OnTowerDamaged(int damage)
    {
        int brokenWeapons = _weapons.Count(weapon => weapon.Broken);

        if (brokenWeapons >= _maxBrokenWeaponCount || _weapons.Count - brokenWeapons <= 1)
        {
            _damage = 0;
            return;
        }
                    
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