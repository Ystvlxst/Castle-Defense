using UnityEngine;

public class RapidBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            Collide();

        if (other.TryGetComponent(out Enemy enemy))
        {
            if (!enemy.IsDying)
                enemy.TakeDamage(Damage);
            else
                return;

            Collide();
        }
    }
}
