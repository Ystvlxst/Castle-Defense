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
                rigidbody.gameObject.transform.DORotate(transform.rotation.eulerAngles, 1);
                rigidbody.AddForce(direction * Force, ForceMode.Impulse);
            }
        }
    }
}
