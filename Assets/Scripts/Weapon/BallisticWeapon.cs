using System;
using System.Collections;
using BabyStack.Model;
using UnityEngine;
using DG.Tweening;

public class BallisticWeapon : Weapon
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _angle;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _torqueForce;

    private readonly float _g = Physics.gravity.y;
    private Vector3 _selectTarget;
    private Vector3 _targetDirection;

    private void Start() => 
        StartCoroutine(Shoot());

    private void Update() =>
            Rotate();

    private IEnumerator Shoot()
    {
        while (true)
        {
            if(CanRefillAmmo())
                RefillAmmo((int)(ShotsPerAmmo * UpgradeFactor));

            while (CanShoot())
            {
                _selectTarget = TargetSelector.SelectTarget();

                Shot();

                yield return new WaitForSeconds(_cooldown / UpgradeFactor);
            }

            yield return null;
        }
    }

    private void Shot()
    {
        if (_targetDirection == _selectTarget)
        {
            Vector3 fromTo = _selectTarget - WeaponTransform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, fromTo.y * 0.5f, fromTo.z);

            float x = fromToXZ.magnitude;
            float y = fromTo.y;

            float angleInRadians = _angle * Mathf.PI / 180;

            float v2 = (_g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));

            Bullet bullet = Instantiate(_bulletTemplate, Spawn.position, Quaternion.identity);
            bullet.Rigidbody.velocity = Spawn.forward * v;
            bullet.Rigidbody.AddTorque(Spawn.forward * _torqueForce);
            MinusAmmo();
            ShotEffect.Play();
        }
    }

    private void Rotate()
    {
        if (_selectTarget == null)
            return;

        _targetDirection = _selectTarget - Vector3.up * 0.5f - WeaponTransform.position;

        float rotationSpeed = _rotationSpeed * UpgradeFactor * UpgradeFactor * Time.deltaTime;
        Spawn.Rotate(Vector3.Cross(Spawn.forward, _targetDirection), rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_selectTarget, 1f);
    }
}