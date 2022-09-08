using UnityEngine;

public class RapidBullet : Bullet
{
    [SerializeField] private ParticleSystem _hitEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            Collide();

        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Collide();
        }
    }

    private void Collide()
    {
        _hitEffect.transform.SetParent(null);
        _hitEffect.Play();
        Destroy(gameObject);
    }
}
