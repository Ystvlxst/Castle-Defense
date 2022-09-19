using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponsBreaker : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private int _damageToBreakWeapon = 150;

    private readonly List<Weapon> _weapons = new List<Weapon>();
    private int _damage;
    private int _maxBrokenWeaponCount = 2;

    private void OnEnable() =>
        _tower.Damaged += OnTowerDamaged;

    private void OnDisable() =>
        _tower.Damaged -= OnTowerDamaged;

    public void Add(Weapon reference) =>
        _weapons.Add(reference);

    private void OnTowerDamaged(int damage)
    {
        int brokenWeapons = _weapons.Count(weapon => weapon.Breakdown.Broken);

        if (brokenWeapons >= _maxBrokenWeaponCount || _weapons.Count - brokenWeapons <= 1)
        {
            _damage = 0;
            return;
        }

        _damage += damage;

        if (_damage >= _damageToBreakWeapon)
        {
            Weapon weapon = _weapons
                .Where(weapon => weapon.Breakdown.Broken == false)
                .OrderBy(weapon => weapon.Ammo)
                .FirstOrDefault();

            if (weapon != null)
            {
                weapon.Breakdown.Break();
                _damage = 0;
            }
        }
    }

    public bool AllWeaponsCantShoot() => 
        _weapons.TrueForAll(weapon => weapon.CanShoot() == false);
}