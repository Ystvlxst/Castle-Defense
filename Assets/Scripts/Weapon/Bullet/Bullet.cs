using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;

    public ParticleSystem HitEffect => _hitEffect;
    public MeshRenderer MeshRenderer => _meshRenderer;
    public Rigidbody Rigidbody => _rigidbody;
    public int Damage => _damage;
    public float Force => _force;
    public float DamageRadius => _damageRadius;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Collision()
    {
        _meshRenderer.enabled = false;
        _hitEffect.Play();
        Destroy(gameObject, 1);
    }
}
