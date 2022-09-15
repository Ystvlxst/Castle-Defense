using System.Collections;
using System.Linq;
using UnityEngine;

public class BallisticWeapon : Weapon
{
    [SerializeField] private float _angle;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _torqueForce;
    [SerializeField] private float _force;

    private readonly float _g = Physics.gravity.y;
    private Vector3 _selectTarget;
    private Vector3 _targetDirection;

    private void Start() =>
        StartCoroutine(Shoot());

    private void Update()
    {
        if(_targetDirection == Vector3.zero)
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
                _selectTarget = TargetSelector.SelectTarget();
                Gunpoint gunpoint = SelectGunpoint();
                _targetDirection = _selectTarget - Vector3.up * 0.5f - gunpoint.transform.position;

                yield return new WaitUntil(() => WeaponJoints.All(joint => joint.LooksAt(_targetDirection)));
                Shot(gunpoint);

                yield return new WaitForSeconds(_cooldown / UpgradeFactor);
            }

            yield return null;
        }
    }

    private void Shot(Gunpoint gunpoint)
    {
        /*Vector3 fromTo = _selectTarget - WeaponTransform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, fromTo.y * 0.5f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = _angle * Mathf.PI / 180;

        float v2 = (_g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));*/
        
        gunpoint.Shoot(_bulletTemplate, _force, _torqueForce);
        MinusAmmo();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_selectTarget, 1f);
    }
}