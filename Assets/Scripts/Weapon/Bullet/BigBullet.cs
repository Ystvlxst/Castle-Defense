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
                if (!enemy.IsDying)
                {
                    enemy.TakeDamage(Damage);
                }
                else
                {
                    foreach (Rigidbody rigidbody in enemy.RigidBodies)
                        rigidbody.AddForce(-(transform.position - collider.transform.position).normalized * Force, ForceMode.Impulse);
                    return;
                }
            }
        }
    }
}
