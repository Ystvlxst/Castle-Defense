using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField] private float _damageRadius;

    protected override void HitGround()
    {
        base.HitGround();
        Explode();
    }

    private void Explode()
    {
        foreach (Collider hitCollider in GetHitColliders())
        {
            if (TryApplyDamage(hitCollider))
                continue;

            TryThrow(hitCollider, GetThrowDirection(hitCollider));
        }
    }

    private Collider[] GetHitColliders() => 
        Physics.OverlapSphere(transform.position, _damageRadius);

    private Vector3 GetThrowDirection(Collider hitCollider) => 
        (hitCollider.transform.position - transform.position).normalized;
}