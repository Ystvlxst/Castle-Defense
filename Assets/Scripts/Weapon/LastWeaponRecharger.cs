using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class LastWeaponRecharger : MonoBehaviour
{
    private Weapon _weapon;
    private WeaponsBreaker _weaponsBreaker;
    
    private void Awake() => 
        _weapon = GetComponent<Weapon>();

    public void Init(WeaponsBreaker weaponsBreaker) => 
        _weaponsBreaker = weaponsBreaker;

    private void Update()
    {
        if (_weaponsBreaker.AllWeaponsCantShoot())
            _weapon.AddAmmo(1);
    }
}
