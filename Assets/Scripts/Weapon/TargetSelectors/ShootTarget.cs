using UnityEngine;

public class ShootTarget
{
    private readonly Transform _transform;
    private readonly IDamageable _damageable;
    private readonly Vector3 _position = Vector3.zero;

    public ShootTarget(IDamageable damagable, Transform transform)
    {
        _transform = transform;
        _damageable = damagable;
    }

    public ShootTarget(IDamageable damageable, Vector3 position)
    {
        _position = position;
        _damageable = damageable;
    }

    public Vector3 Position => _transform != null ? _transform.position : _position;
    public bool Dead => _damageable.Dead;
}