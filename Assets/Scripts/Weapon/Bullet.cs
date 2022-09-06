using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _distructible;

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;

    public float DamageRadius { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        DamageRadius = 1;
    }

    private void Update()
    {
        CheckDistructibles();
    }

    private void CheckDistructibles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius, _distructible);

        foreach(var collider in colliders)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
                Destroy(gameObject, 0.1f);
            }

            if (collider.TryGetComponent(out Ground ground))
                Destroy(gameObject, 0.1f);
        }
    }
}
