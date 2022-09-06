using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void CheckDistructibles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _damageRadius);

        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground) || other.TryGetComponent(out Enemy enemy))
        {
            CheckDistructibles();
            Destroy(gameObject);
        }
    }
}
