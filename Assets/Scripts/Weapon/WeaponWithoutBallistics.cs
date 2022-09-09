using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponWithoutBallistics : MonoBehaviour
{

    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _template;
    [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _shotsPerAmmo;
    [SerializeField] private ParticleSystem _shotEffect;

    private int _ammo;
    private float _cooldownFactor = 1;

    private void Start() =>
        StartCoroutine(Shoot());

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (CanRefillAmmo())
                RefillAmmo();

            while (CanShoot())
            {
                Shot();

                yield return new WaitForSeconds(_cooldown / _cooldownFactor);
            }

            yield return null;
        }
    }

    private bool CanShoot() =>
        _ammo > 0 && _targetSelector.HasTarget;

    private bool CanRefillAmmo() =>
        _stackPresenter.Empty == false && _ammo == 0;

    private void RefillAmmo()
    {
        Stackable stackable = _stackPresenter.Data.Last();
        _stackPresenter.RemoveFromStack(stackable);
        Destroy(stackable.gameObject);
        _ammo = _shotsPerAmmo;
    }

    private void Shot()
    {
        _ammo--;
        _shotEffect.Play();

        Vector3 fromTo = _targetSelector.SelectTarget() - _weaponTransform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, fromTo.y, fromTo.z);

        _weaponTransform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        Bullet bullet = Instantiate(_template, _spawn.position, Quaternion.identity);
        bullet.Rigidbody.velocity = _spawn.forward * _speed;
    }

    public void OnModificationUpdate(float value)
    {
        _cooldownFactor = value;
    }
}
