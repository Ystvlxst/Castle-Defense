using System.Collections;
using BabyStack.Model;
using UnityEngine;

public class BallisticWeapon : Weapon, IModificationListener<float>
{
    [SerializeField] private float _angle;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _torqueForce;

    private readonly float _g = Physics.gravity.y;

    private void Start() => 
        StartCoroutine(Shoot());

    private IEnumerator Shoot()
    {
        while (true)
        {
            if(CanRefillAmmo())
                RefillAmmo();
            
            while (CanShoot())
            {
                Shot();
                
                yield return new WaitForSeconds(_cooldown / CooldownFactor);
            }

            yield return null;
        }
    }

    private void Shot()
    {
        MinusAmmo();
        ShotEffect.Play();
        
        Spawn.localEulerAngles = new Vector3(-_angle, 0f, 0f);

        Vector3 fromTo = TargetSelector.SelectTarget() - WeaponTransform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        WeaponTransform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = _angle * Mathf.PI / 180;

        float v2 = (_g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        Bullet bullet = Instantiate(_bulletTemplate, Spawn.position, Quaternion.identity);
        bullet.Rigidbody.velocity = Spawn.forward * v;
        bullet.Rigidbody.AddTorque(Spawn.forward * _torqueForce);
    }
}