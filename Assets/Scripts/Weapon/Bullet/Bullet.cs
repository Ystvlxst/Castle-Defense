using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _hitThrowForce;
    [SerializeField] private ParticleSystem _hitEffectTemplate;
    [SerializeField] private ParticleSystem _groundDecal;

    private Rigidbody _rigidbody;

    public void Init(Vector3 velocity, Vector3 torque)
    {
        _rigidbody.velocity = velocity;
        _rigidbody.AddTorque(torque);
    }

    private void Awake() => 
        _rigidbody = GetComponent<Rigidbody>();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground _)) 
            HitGround();
    }

    protected virtual void HitGround()
    {
        SpawnDecalEffect(_groundDecal);
        Collide();
    }

    protected void Collide()
    {
        ParticleSystem effect = Instantiate(_hitEffectTemplate, transform.position, Quaternion.identity);
        effect.Play();
        Destroy(gameObject);
    }

    protected void TryThrow(Collider collider, Vector3 direction)
    {
        if (!collider.TryGetComponent(out IThrowable rigidbody))
            return;

        rigidbody.Throw(direction * _hitThrowForce);
    }

    protected bool TryApplyDamage(Collider collider)
    {
        if (!collider.TryGetComponent(out IDamageable enemy))
            return true;

        enemy.TakeDamage(_damage);
        
        return false;
    }

    private void SpawnDecalEffect(ParticleSystem particleSystem) => 
        Instantiate(particleSystem, transform.position, Quaternion.identity);
}
