using System.Collections;
using UnityEngine;

public class BallisticWeapon : Weapon
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _torqueForce;
    [SerializeField] private float _force;
    [SerializeField] private float _verticalOffset;

    private Vector3 _aimDirection;

    private void Start() =>
        StartCoroutine(Shoot());

    private void Update()
    {
        if (_aimDirection == Vector3.zero)
            return;

        Aim(_aimDirection);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (CanRefillAmmo())
                RefillAmmo((int) (ShotsPerAmmo * UpgradeFactor));

            while (CanShoot())
            {
                Gunpoint gunpoint = SelectGunpoint();
                ShootTarget shootTarget = TargetSelector.SelectTarget();

                _aimDirection = GetAimDirection(shootTarget, gunpoint);

                yield return new WaitUntil(() => Aimed(_aimDirection));

                MakeShot(gunpoint);

                yield return WaitForCooldown();
            }

            yield return null;
        }
    }

    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(_cooldown / UpgradeFactor);
    }

    private Vector3 GetAimDirection(ShootTarget shootTarget, Gunpoint gunpoint) =>
        shootTarget.Position + Vector3.up * _verticalOffset - gunpoint.transform.position;

    private void MakeShot(Gunpoint gunpoint)
    {
        gunpoint.Shoot(_bulletTemplate, _force, _torqueForce);
        SpendAmmo();
    }
}