using System.Collections;
using System.Linq;
using UnityEngine;

public class BallisticWeapon : Weapon
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _torqueForce;
    [SerializeField] private float _force;
    [SerializeField] private float _verticalOffset;

    private readonly float _g = Physics.gravity.y;
    private ShootTarget _shootTarget;
    private Vector3 _targetDirection;

    private void Start() =>
        StartCoroutine(Shoot());

    private void Update()
    {
        if (_targetDirection == Vector3.zero)
            return;

        foreach (WeaponJoint joint in WeaponJoints)
            joint.Rotate(_targetDirection);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (CanRefillAmmo())
                RefillAmmo((int) (ShotsPerAmmo * UpgradeFactor));

            while (CanShoot())
            {
                _shootTarget = TargetSelector.SelectTarget();
                Gunpoint gunpoint = SelectGunpoint();
                _targetDirection = _shootTarget.Position + Vector3.up * _verticalOffset - gunpoint.transform.position;

                yield return new WaitUntil(() => WeaponJoints.All(joint => joint.LooksAt(_targetDirection)));
                Shot(gunpoint);

                yield return new WaitForSeconds(_cooldown / UpgradeFactor);
            }

            yield return null;
        }
    }

    private void Shot(Gunpoint gunpoint)
    {
        gunpoint.Shoot(_bulletTemplate, _force, _torqueForce);
        MinusAmmo();
    }
}