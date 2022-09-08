using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : Bullet
{
    private void CheckDistructibles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);

                if (enemy.IsDying)
                    enemy.TakeImpulseForce(Force);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground) || other.TryGetComponent(out Enemy enemy))
        {
            Collision();
            CheckDistructibles();
        }
    }
}
