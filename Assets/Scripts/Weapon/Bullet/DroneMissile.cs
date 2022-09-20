using UnityEngine;

class DroneMissile : ExplosiveBullet
{
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private float _rotationMultiplier = 10;
    [SerializeField] private float _acceleration = 5;

    private float _currentSpeed;
    private Transform _target;
    private Vector3 _lastTargetPosition = Vector3.zero;
    private Vector3 _targetPosition => _target != null ? _target.position + Vector3.down * 5f : _lastTargetPosition;

    private void FixedUpdate()
    {
        _currentSpeed = Mathf.Lerp(_currentSpeed, _maxSpeed, _acceleration * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(_targetPosition + Vector3.up - transform.position);
        float rotationSpeed = _currentSpeed * _rotationMultiplier * Time.deltaTime;
        Quaternion rotation = Quaternion.RotateTowards(Rigidbody.rotation, lookRotation, rotationSpeed);
        Rigidbody.MoveRotation(rotation);

        float currentSpeed = _currentSpeed * Time.deltaTime;
        Vector3 targetPosition = Rigidbody.position + currentSpeed * (Rigidbody.rotation * Vector3.forward);
        Rigidbody.MovePosition(targetPosition);
        _lastTargetPosition = _targetPosition;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if(!TryApplyDamage(other))
            return;

        Explode();
        Collide();
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}