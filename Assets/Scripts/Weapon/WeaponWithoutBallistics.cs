using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class WeaponWithoutBallistics : Weapon
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
    [SerializeField] private float _rotationSpeed;

    private int _ammo;
    private float _timePerAmmo = 5;
    private float _cooldownFactor = 1;
    private Enemy _target;

    private void Start() =>
        StartCoroutine(Shoot());

    private void Update()
    {
        Rotate();
    }

    private IEnumerator Shoot()
    {
        float time = 0;

        while (true)
        {
            yield return null;
            
            if (CanRefillAmmo())
                RefillAmmo();

            if(CanShoot() && (_target == null || _target.IsDying))
                _target = _targetSelector.SelectEnemyTarget();

            while (CanShoot() && _target != null)
            {
                if(_target.IsDying)
                    break;
                
                Shot(_target.transform.position);
                time += Time.deltaTime;

                if (time > _timePerAmmo)
                {
                    _ammo--;
                    time = 0;
                }

                yield return new WaitForSeconds(_cooldown / _cooldownFactor);
            }

            yield return null;
        }
    }

    private void Rotate()
    {
        if(_target == null)
            return;

        Vector3 targetDirection = _target.transform.position - Vector3.up * 0.5f - _weaponTransform.position;
        
        _spawn.Rotate(Vector3.Cross(targetDirection, _spawn.forward), _rotationSpeed * Time.deltaTime);
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

    private void Shot(Vector3 targetPosition)
    {
        _shotEffect.Play();

        Bullet bullet = Instantiate(_template, _spawn.position, Quaternion.identity);
        bullet.Rigidbody.velocity = _spawn.forward * _speed;
    }

    public void OnModificationUpdate(float value)
    {
        _cooldownFactor = value;
    }
}
