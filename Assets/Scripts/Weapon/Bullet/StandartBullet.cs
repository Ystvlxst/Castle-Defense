using UnityEngine;

public class StandartBullet : Bullet
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(!TryApplyDamage(other))
            return;
        
        TryThrow(other, transform.forward);
        Collide();
    }
}
