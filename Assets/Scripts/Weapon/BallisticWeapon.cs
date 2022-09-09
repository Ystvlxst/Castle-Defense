using System.Collections;
using System.Linq;
using BabyStack.Model;
using UnityEngine;

public class BallisticWeapon : Weapon, IModificationListener<float>
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private float _angle;
    [SerializeField] private Bullet _template;
    [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private StackPresenter _stackPresenter;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _shotsPerAmmo;
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private float _torqueForce;

    private readonly float _g = Physics.gravity.y;
    private int _ammo;
    private float _cooldownFactor = 1;

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
        
        _spawn.localEulerAngles = new Vector3(-_angle, 0f, 0f);

        Vector3 fromTo = _targetSelector.SelectTarget() - _weaponTransform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        _weaponTransform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = _angle * Mathf.PI / 180;

        float v2 = (_g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        Bullet bullet = Instantiate(_template, _spawn.position, Quaternion.identity);
        bullet.Rigidbody.velocity = _spawn.forward * v;
        bullet.Rigidbody.AddTorque(_spawn.forward * _torqueForce);
    }

    public void OnModificationUpdate(float value)
    {
        _cooldownFactor = value;
    }
}