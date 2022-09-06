using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshFilter))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private MeshFilter _meshFilter;
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;

    public float DamageRadius { get; set; }

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _rigidbody = GetComponent<Rigidbody>();
        DamageRadius = 1;
    }

    private void Update()
    {
        CheckDistructibles();
    }

    private void CheckDistructibles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);

        foreach(var collider in colliders)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
                _meshFilter.mesh = null;
                Destroy(gameObject, 0.1f);
            }

            if (collider.TryGetComponent(out Ground ground))
            {
                _meshFilter.mesh = null;
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
