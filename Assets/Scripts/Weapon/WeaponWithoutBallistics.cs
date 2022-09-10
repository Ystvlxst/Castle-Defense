using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BabyStack.Model;
using DG.Tweening;
using UnityEngine;

public class WeaponWithoutBallistics : Weapon, IModificationListener<float>
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private float _timePerAmmo = 5;
    private Enemy _target;

    private void Start() =>
        StartCoroutine(Shoot());

    private void Update() =>
        Rotate();

    private IEnumerator Shoot()
    {
        float time = 0;

        while (true)
        {
            yield return null;
            
            if (CanRefillAmmo())
                RefillAmmo();

            if(CanShoot() && (_target == null || _target.IsDying))
                _target = TargetSelector.SelectEnemyTarget();

            while (CanShoot() && _target != null)
            {
                if(_target.IsDying)
                    break;
                
                Shot(_target.transform.position);
                time += Time.deltaTime;

                if (time > _timePerAmmo)
                {
                    MinusAmmo();
                    time = 0;
                }

                yield return new WaitForSeconds(Cooldown / CooldownFactor);
            }

            yield return null;
        }
    }

    private void Rotate()
    {
        if(_target == null)
            return;

        Vector3 targetDirection = _target.transform.position - Vector3.up * 0.5f - WeaponTransform.position;

        Spawn.Rotate(Vector3.Cross(targetDirection, Spawn.forward), _rotationSpeed * Time.deltaTime);
    }

    private void Shot(Vector3 targetPosition)
    {
        ShotEffect.Play();

        Bullet bullet = Instantiate(Template, Spawn.position, Quaternion.identity);
        bullet.Rigidbody.velocity = Spawn.forward * _speed;
    }
}
