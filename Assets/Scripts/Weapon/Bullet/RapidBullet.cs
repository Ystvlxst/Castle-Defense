using UnityEngine;

public class RapidBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            Collision();

        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Collision();
        }
    }
}
