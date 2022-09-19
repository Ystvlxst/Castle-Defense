using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class LastWeaponRecharger : MonoBehaviour
{
    private Weapon _weapon;
    private WeaponsBreaker _weaponsBreaker;

    public void Init(WeaponsBreaker weaponsBreaker) => 
        _weaponsBreaker = weaponsBreaker;

    private void Awake() => 
        _weapon = GetComponent<Weapon>();

    private void Start()
    {
        StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        while (true)
        {
            yield return new WaitUntil(() => _weapon.Ammo == 0 && _weaponsBreaker.AllWeaponsCantShoot());

            _weapon.AddAmmo(30);
            
            yield return new WaitUntil(() => !_weapon.CanShoot());
            yield return new WaitUntil(() => _weapon.CanShoot());

            yield return null;
        }
    }
}
