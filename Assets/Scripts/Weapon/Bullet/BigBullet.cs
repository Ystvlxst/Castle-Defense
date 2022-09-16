using DG.Tweening;
using UnityEngine;

public class BigBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
        {
            Collide();
            Explode();
            DecalEffect(GroundDecal);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);

        foreach (Collider collider in colliders)
        {
            if (!collider.TryGetComponent(out IDamageable enemy))
                continue;
            
            enemy.TakeDamage(Damage);
            
            
            if (collider.TryGetComponent(out IThrowable rigidbody))
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rigidbody.Throw(direction * Force);
            }
        }
    }
}