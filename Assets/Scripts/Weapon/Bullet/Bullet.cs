using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;
    [SerializeField] private float _force;
    [SerializeField] private ParticleSystem _hitEffectTemplate;
    [SerializeField] private ParticleSystem _groundDecal;

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;
    public float Force => _force;
    public float DamageRadius => _damageRadius;
    public ParticleSystem GroundDecal => _groundDecal;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Collide()
    {
        ParticleSystem effect = Instantiate(_hitEffectTemplate, transform.position, Quaternion.identity);
        effect.Play();
        Destroy(gameObject);
    }

    public void DecalEffect(ParticleSystem particleSystem)
    {
        ParticleSystem effect = Instantiate(particleSystem, transform.position, Quaternion.identity);
    }
}
