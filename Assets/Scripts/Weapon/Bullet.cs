using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;
    [SerializeField] private ParticleSystem _hitEffect;

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
                enemy.TakeDamage(_damage);

            //if (collider.TryGetComponent(out Rigidbody rigidbody))
                //rigidbody.AddForce(-collider.transform.position * _damageRadius * _damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground) || other.TryGetComponent(out Enemy enemy))
        {
            _meshRenderer.enabled = false;
            CheckDistructibles();
            _hitEffect.Play();
            Destroy(gameObject, 1f);
        }
    }
}
