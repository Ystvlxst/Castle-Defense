using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground) || other.TryGetComponent(out Enemy enemy))
        {
            Collide();
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable enemy)) 
                enemy.TakeDamage(Damage);
            
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * Force, ForceMode.Impulse);
            }
        }
    }
}
