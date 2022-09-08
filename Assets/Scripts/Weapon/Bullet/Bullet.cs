using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;
    public float Force => _force;
    public float DamageRadius => _damageRadius;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
