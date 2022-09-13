using UnityEngine;

public class RapidBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            Collide();

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(Damage);
            Collide();
            
            if (other.TryGetComponent(out IThrowable throwable)) 
                throwable.Throw(transform.forward);
        }
    }
}
