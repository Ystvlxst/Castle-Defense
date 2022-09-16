using System.Collections;
using BabyStack.Model;
using UnityEngine;

public class LaserWeapon : Weapon
{
    [SerializeField] private float _damage;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ParticleSystem _laserShotEffectTemplate;
    [SerializeField] private float _timePerAmmo = 5;
    
    private Enemy _target;
    private Gunpoint _gunpoint;

    private void Start()
    {
        _gunpoint = SelectGunpoint();
        StartCoroutine(Shoot());
    }

    private void Update() =>
        Rotate();

    private IEnumerator Shoot()
    {
        float time = 0;

        while (true)
        {
            yield return null;
            
            if (CanRefillAmmo())
                RefillAmmo(1);

            ResetBeam();

            if(CanShoot() && (_target == null || _target.IsDying))
                _target = TargetSelector.SelectEnemyTarget();

            while (CanShoot() && _target != null)
            {
                if(_target.IsDying)
                    break;
                
                Shot();
                time += Time.deltaTime;

                if (time > _timePerAmmo * UpgradeFactor)
                {
                    MinusAmmo();
                    time = 0;
                }

                yield return null;
            }

            yield return null;
        }
    }

    private void Rotate()
    {
        if(_target == null)
            return;
        
        Vector3 targetDirection = _target.transform.position - Vector3.up * 0.5f - _gunpoint.transform.position;
        
        foreach (WeaponJoint joint in WeaponJoints) 
            joint.Rotate(targetDirection);
    }

    private void Shot()
    {
        _gunpoint = SelectGunpoint();
        ResetBeam();

        RaycastHit[] results = new RaycastHit[16];

        int count = Physics.RaycastNonAlloc(_gunpoint.transform.position, _gunpoint.transform.forward, results,  1000f,_layerMask);

        for (int i = 0; i < count; i++)
        {
            if (results[i].collider.TryGetComponent(out IDamageable damageable)) 
                damageable.TakeDamage(_damage);
            
            Instantiate(_laserShotEffectTemplate, results[i].point, Quaternion.identity);
        }

        if (count > 0) 
            SetLaserRayEnd(results[0].point);
    }

    private void ResetBeam() => 
        SetLaserRayEnd(_gunpoint.transform.position);

    private void SetLaserRayEnd(Vector3 position)
    {
        _lineRenderer.SetPosition(1, _lineRenderer.transform.InverseTransformPoint(position));
        _lineRenderer.SetPosition(0, _lineRenderer.transform.InverseTransformPoint(_gunpoint.transform.position));
    }
}
