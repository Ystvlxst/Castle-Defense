using System.Collections;
using BabyStack.Model;
using UnityEngine;

public class LaserWeapon : Weapon, IModificationListener<float>
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ParticleSystem _laserShotEffectTemplate;
    [SerializeField] private float _timePerAmmo = 5;
    
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

        Vector3 targetDirection = _target.transform.position - Vector3.up * 0.5f - WeaponTransform.position;

        float rotationSpeed = _rotationSpeed * UpgradeFactor * UpgradeFactor * Time.deltaTime;
        Spawn.Rotate(Vector3.Cross(Spawn.forward, targetDirection), rotationSpeed);
    }

    private void Shot()
    {
        ResetBeam();
        ShotEffect.Play();

        RaycastHit[] results = new RaycastHit[16];

        int count = Physics.RaycastNonAlloc(Spawn.position, Spawn.forward, results,  1000f,_layerMask);

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
        SetLaserRayEnd(Spawn.position);

    private void SetLaserRayEnd(Vector3 position)
    {
        _lineRenderer.SetPosition(1, _lineRenderer.transform.InverseTransformPoint(position));
    }
}