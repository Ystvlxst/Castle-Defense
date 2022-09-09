using System;
using UnityEngine;
using BabyStack.Model;

[RequireComponent(typeof(AIMovement))]
public class CharacterMovement : MonoBehaviour, IModificationListener<float>
{
    [SerializeField] private DoctorAnimation _animation;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private float _speed;
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;

    private AIMovement _movement;
    private float _speedRate = 1f;
    private float _flySpeedRate = 1f;
    private IInputSource _inputSource;
    private Vector3 _lastPosition;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _movement = GetComponent<AIMovement>();
        _inputSource = (IInputSource) _inputSourceBehaviour;
    }

    private void Update()
    {
        Move();
    }

    public void Stop()
    {
        if (_movement != null)
            _movement.Move(transform.position);

        _animation.SetSpeed(0);
        IsMoving = false;
    }

    public void OnModificationUpdate(float value)
    {
        _speedRate = value;
    }

    public void SetFlySpeedRate(float rate)
    {
        if (rate <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(rate));

        _flySpeedRate = rate;
    }

    public void SetInput(IInputSource inputSource)
    {
        _inputSource = inputSource;
    }
    
    private void Move()
    {
        _playerModel.LookAt(_playerModel.position + Vector3.ProjectOnPlane(_movement.Velocity, Vector3.up));
        _movement.Move(_inputSource.Destination);

        float distanceToDestination = Vector3.Distance(transform.position, _inputSource.Destination);

        float deltaMovement = Mathf.Clamp01(distanceToDestination);
        _movement.SetSpeed(_speedRate * _speed * deltaMovement);
        _animation.SetSpeed(deltaMovement);
        
        IsMoving = transform.position != _lastPosition;
        _lastPosition = transform.position;
    }
}