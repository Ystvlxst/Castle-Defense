using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;

    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void CheckDistructibles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _damageRadius);

        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                if (enemy.IsDying)
                    enemy.TakeImpulseForce(_force);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
            Collision();

        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Collision();
        }
    }

    private void Collision()
    {
        _meshRenderer.enabled = false;
        CheckDistructibles();
        _hitEffect.Play();
        Destroy(gameObject, 1);
    }
}
