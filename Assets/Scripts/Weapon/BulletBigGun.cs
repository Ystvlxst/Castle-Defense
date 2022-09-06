using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBigGun : MonoBehaviour
{
    [SerializeField] private float _radiusFactor;

    private Bullet _bullet;

    private void Start()
    {
        _bullet = GetComponent<Bullet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground) || other.TryGetComponent(out Enemy enemy))
        {
            _bullet.DamageRadius *= _radiusFactor;
        }
    }
}
