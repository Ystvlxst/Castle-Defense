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
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);

                if (enemy.IsDying)
                    enemy.TakeImpulseForce(Force);
            }
        }
    }
}
